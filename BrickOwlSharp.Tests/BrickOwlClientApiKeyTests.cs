#region License
// Copyright (c) 2024 Stephan Stapel
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
# endregion
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using BrickOwlSharp.Client;
using Xunit;

namespace BrickOwlSharp.Tests;

public class BrickOwlClientApiKeyTests
{
    private sealed class CapturingHandler : HttpMessageHandler
    {
        public string? CapturedUrl { get; private set; }
        public string? CapturedBody { get; private set; }
        private readonly string _responseJson;

        public CapturingHandler(string responseJson)
        {
            _responseJson = responseJson;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            CapturedUrl = request.RequestUri?.ToString();
            if (request.Content != null)
                CapturedBody = await request.Content.ReadAsStringAsync();
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(_responseJson)
            };
        }
    }

    private sealed class TrackingHandler : HttpMessageHandler
    {
        public List<string> CapturedUrls { get; } = new();
        private readonly Func<string, string> _responseSelector;

        public TrackingHandler(Func<string, string> responseSelector)
        {
            _responseSelector = responseSelector;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var url = request.RequestUri?.ToString() ?? string.Empty;
            CapturedUrls.Add(url);
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(_responseSelector(url))
            });
        }
    }

    private static (IBrickOwlClient client, CapturingHandler handler) BuildClient(string responseJson)
    {
        var handler = new CapturingHandler(responseJson);
        var client = BrickOwlClientFactory.Build(new HttpClient(handler));
        return (client, handler);
    }

    [Fact]
    public async Task GetOrdersAsync_WithoutApiKey_UsesInstanceApiKey()
    {
        BrickOwlClientConfiguration.Instance.ApiKey = "instance-key";
        var (client, handler) = BuildClient("[]");

        await client.GetOrdersAsync();

        Assert.Contains("key=instance-key", handler.CapturedUrl);
    }

    [Fact]
    public async Task GetOrdersAsync_WithExplicitApiKey_UsesExplicitKey()
    {
        BrickOwlClientConfiguration.Instance.ApiKey = "instance-key";
        var (client, handler) = BuildClient("[]");

        await client.GetOrdersAsync(apiKey: "explicit-key");

        Assert.Contains("key=explicit-key", handler.CapturedUrl);
        Assert.DoesNotContain("key=instance-key", handler.CapturedUrl);
    }

    [Fact]
    public async Task GetOrderAsync_WithExplicitApiKey_UsesSameKeyForBothRequests()
    {
        BrickOwlClientConfiguration.Instance.ApiKey = "instance-key";
        var trackingHandler = new TrackingHandler(url =>
            url.Contains("order/items") ? "[]" : "{}");
        var client = BrickOwlClientFactory.Build(new HttpClient(trackingHandler));

        await client.GetOrderAsync(42, apiKey: "shop-key");

        Assert.Equal(2, trackingHandler.CapturedUrls.Count);
        foreach (var url in trackingHandler.CapturedUrls)
        {
            Assert.Contains("key=shop-key", url);
            Assert.DoesNotContain("key=instance-key", url);
        }
    }

    [Fact]
    public async Task UpdateOrderStatusAsync_WithoutApiKey_UsesInstanceApiKeyInFormData()
    {
        BrickOwlClientConfiguration.Instance.ApiKey = "instance-key";
        var (client, handler) = BuildClient("{\"status\":\"success\"}");

        await client.UpdateOrderStatusAsync(1, OrderStatus.Processing);

        Assert.Contains("key=instance-key", handler.CapturedBody);
    }

    [Fact]
    public async Task UpdateOrderStatusAsync_WithExplicitApiKey_UsesExplicitKeyInFormData()
    {
        BrickOwlClientConfiguration.Instance.ApiKey = "instance-key";
        var (client, handler) = BuildClient("{\"status\":\"success\"}");

        await client.UpdateOrderStatusAsync(1, OrderStatus.Processing, apiKey: "shop2-key");

        Assert.Contains("key=shop2-key", handler.CapturedBody);
        Assert.DoesNotContain("key=instance-key", handler.CapturedBody);
    }

    [Fact]
    public async Task UpdateOrderNoteAsync_WithExplicitApiKey_UsesExplicitKeyInFormData()
    {
        BrickOwlClientConfiguration.Instance.ApiKey = "instance-key";
        var (client, handler) = BuildClient("{\"status\":\"success\"}");

        await client.UpdateOrderNoteAsync(1, "test note", apiKey: "shop3-key");

        Assert.Contains("key=shop3-key", handler.CapturedBody);
        Assert.DoesNotContain("key=instance-key", handler.CapturedBody);
    }

    [Fact]
    public async Task GetOrdersAsync_WithNoKeyAnywhere_ThrowsInvalidOperationException()
    {
        BrickOwlClientConfiguration.Instance.ApiKey = null;
        var (client, _) = BuildClient("[]");

        await Assert.ThrowsAsync<InvalidOperationException>(() => client.GetOrdersAsync());
    }

    [Fact]
    public async Task Build_WithFactoryApiKey_UsesFactoryKeyWhenNoPerCallKey()
    {
        BrickOwlClientConfiguration.Instance.ApiKey = null;
        var handler = new CapturingHandler("[]");
        var client = BrickOwlClientFactory.Build(new HttpClient(handler), apiKey: "factory-key");

        await client.GetOrdersAsync();

        Assert.Contains("key=factory-key", handler.CapturedUrl);
    }

    [Fact]
    public async Task Build_WithFactoryApiKey_PerCallKeyOverridesFactoryKey()
    {
        BrickOwlClientConfiguration.Instance.ApiKey = null;
        var handler = new CapturingHandler("[]");
        var client = BrickOwlClientFactory.Build(new HttpClient(handler), apiKey: "factory-key");

        await client.GetOrdersAsync(apiKey: "override-key");

        Assert.Contains("key=override-key", handler.CapturedUrl);
        Assert.DoesNotContain("key=factory-key", handler.CapturedUrl);
    }

    [Fact]
    public async Task GetOrdersAsync_WithMinUpdateTime_AppendsUpdateTimeAsUnixTimestamp()
    {
        BrickOwlClientConfiguration.Instance.ApiKey = "test-key";
        var (client, handler) = BuildClient("[]");
        var updateTime = new DateTime(2024, 1, 15, 12, 0, 0, DateTimeKind.Utc);
        var expectedTimestamp = ((DateTimeOffset)updateTime).ToUnixTimeSeconds();

        await client.GetOrdersAsync(minUpdateTime: updateTime);

        Assert.Contains($"update_time={expectedTimestamp}", handler.CapturedUrl);
    }

    [Fact]
    public async Task UpdateInventoryAsync_ForSaleFalse_SendsForSaleZeroInBody()
    {
        BrickOwlClientConfiguration.Instance.ApiKey = "test-key";
        var (client, handler) = BuildClient("{\"status\":\"success\"}");

        await client.UpdateInventoryAsync(new UpdateInventory { ForSale = false });

        Assert.Contains("for_sale=0", handler.CapturedBody);
    }

    [Fact]
    public async Task UpdateInventoryAsync_ForSaleTrue_SendsForSaleOneInBody()
    {
        BrickOwlClientConfiguration.Instance.ApiKey = "test-key";
        var (client, handler) = BuildClient("{\"status\":\"success\"}");

        await client.UpdateInventoryAsync(new UpdateInventory { ForSale = true });

        Assert.Contains("for_sale=1", handler.CapturedBody);
    }

    [Fact]
    public async Task UpdateInventoryAsync_ExternalIdSet_SendsExternalIdStringInBody()
    {
        // ExternalId is a caller-supplied opaque string tag (e.g. a GUID from the caller's own
        // system) that BrickOwl's API stores and echoes back verbatim - it was previously typed
        // as int?, which could not represent a non-numeric external id and was inconsistent with
        // NewInventory.ExternalId, which is already a string.
        BrickOwlClientConfiguration.Instance.ApiKey = "test-key";
        var (client, handler) = BuildClient("{\"status\":\"success\"}");

        await client.UpdateInventoryAsync(new UpdateInventory { ExternalId = "3f9a2b7c-1d4e-4a5b-9c6d-7e8f9a0b1c2d" });

        Assert.Contains("external_id=3f9a2b7c-1d4e-4a5b-9c6d-7e8f9a0b1c2d", handler.CapturedBody);
    }
}
