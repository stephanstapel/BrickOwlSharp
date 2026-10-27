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
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using BrickOwlSharp.Client;
using Xunit;

namespace BrickOwlSharp.Tests;

public class BrickOwlClientErrorHandlingTests
{
    private const string ApiKey = "secret-api-key";

    private static IBrickOwlClient BuildClient(HttpStatusCode statusCode, string body, IBrickOwlRequestHandler? requestHandler = null)
    {
        return BrickOwlClientFactory.Build(new HttpClient(StubHttpMessageHandler.Returning(statusCode, body)), requestHandler: requestHandler);
    }

    private static IBrickOwlClient BuildHangingClient()
    {
        var handler = new StubHttpMessageHandler(async (_, ct) =>
        {
            await Task.Delay(Timeout.Infinite, ct);
            return new HttpResponseMessage(HttpStatusCode.OK);
        });
        return BrickOwlClientFactory.Build(new HttpClient(handler));
    }

    [Fact]
    public async Task UpdateInventoryAsync_ErrorStatus_DoesNotLeakApiKeyInExceptionMessage()
    {
        using var client = BuildClient(HttpStatusCode.BadRequest, "{\"error\":\"bad\"}");

        var ex = await Assert.ThrowsAsync<HttpRequestException>(() => client.UpdateInventoryAsync(new UpdateInventory { LotId = 1 }, apiKey: ApiKey));

        Assert.DoesNotContain(ApiKey, ex.Message);
        Assert.Contains("\"key\":\"***\"", ex.Message);
        Assert.Contains("{\"error\":\"bad\"}", ex.Message);
    }

    [Fact]
    public async Task UpdateInventoryAsync_ErrorStatusWithNonJsonBody_ThrowsHttpRequestExceptionWithStatusAndBody()
    {
        using var client = BuildClient(HttpStatusCode.InternalServerError, "<html>oops</html>");

        var ex = await Assert.ThrowsAsync<HttpRequestException>(() => client.UpdateInventoryAsync(new UpdateInventory { LotId = 1 }, apiKey: ApiKey));

        Assert.Contains("InternalServerError", ex.Message);
        Assert.Contains("<html>oops</html>", ex.Message);
    }

    [Fact]
    public async Task UpdateInventoryAsync_SuccessStatusWithInvalidJson_ThrowsHttpRequestExceptionWithJsonInnerException()
    {
        using var client = BuildClient(HttpStatusCode.OK, "not json");

        var ex = await Assert.ThrowsAsync<HttpRequestException>(() => client.UpdateInventoryAsync(new UpdateInventory { LotId = 1 }, apiKey: ApiKey));

        Assert.IsAssignableFrom<JsonException>(ex.InnerException);
        Assert.DoesNotContain(ApiKey, ex.Message);
    }

    [Fact]
    public async Task GetOrdersAsync_ErrorStatus_IncludesResponseBodyButNotApiKey()
    {
        using var client = BuildClient(HttpStatusCode.Forbidden, "{\"error\":\"Invalid key\"}");

        var ex = await Assert.ThrowsAsync<HttpRequestException>(() => client.GetOrdersAsync(apiKey: ApiKey));

        Assert.Contains("Invalid key", ex.Message);
        Assert.DoesNotContain(ApiKey, ex.Message);
    }

    [Fact]
    public async Task GetOrdersAsync_NullResponse_ThrowsHttpRequestException()
    {
        using var client = BuildClient(HttpStatusCode.OK, "null");

        await Assert.ThrowsAsync<HttpRequestException>(() => client.GetOrdersAsync(apiKey: ApiKey));
    }

    [Fact]
    public async Task UpdateOrderStatusAsync_ErrorStatus_ReturnsFalse()
    {
        using var client = BuildClient(HttpStatusCode.InternalServerError, "");

        bool result = await client.UpdateOrderStatusAsync(1, OrderStatus.Processing, apiKey: ApiKey);

        Assert.False(result);
    }

    [Fact]
    public async Task UpdateOrderStatusAsync_Cancelled_ThrowsOperationCanceledException()
    {
        using var client = BuildHangingClient();
        using var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(50));

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => client.UpdateOrderStatusAsync(1, OrderStatus.Processing, apiKey: ApiKey, cancellationToken: cts.Token));
    }

    [Fact]
    public async Task UpdateInventoryAsync_Cancelled_ThrowsOperationCanceledException()
    {
        using var client = BuildHangingClient();
        using var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(50));

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => client.UpdateInventoryAsync(new UpdateInventory { LotId = 1 }, apiKey: ApiKey, cancellationToken: cts.Token));
    }

    [Fact]
    public async Task GetOrdersAsync_WithRequestHandler_AwaitsHandlerBeforeReturning()
    {
        var requestHandler = new RecordingRequestHandler();
        using var client = BuildClient(HttpStatusCode.OK, "[]", requestHandler);

        await client.GetOrdersAsync(apiKey: ApiKey);

        Assert.Equal(1, requestHandler.CompletedCalls);
    }

    [Fact]
    public async Task GetOrdersAsync_RequestHandlerThrows_ExceptionReachesCaller()
    {
        var requestHandler = new RecordingRequestHandler(new InvalidOperationException("rate limit exceeded"));
        using var client = BuildClient(HttpStatusCode.OK, "[]", requestHandler);

        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => client.GetOrdersAsync(apiKey: ApiKey));

        Assert.Equal("rate limit exceeded", ex.Message);
    }

    [Fact]
    public async Task Dispose_WithExternalHttpClient_DoesNotDisposeIt()
    {
        using var httpClient = new HttpClient(StubHttpMessageHandler.Returning(HttpStatusCode.OK, "[]"));
        var client = BrickOwlClientFactory.Build(httpClient);

        client.Dispose();

        using var response = await httpClient.GetAsync("https://example.invalid/");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetColorListAsync_ReturnsColorsFromDictionaryResponse()
    {
        using var client = BuildClient(HttpStatusCode.OK, "{\"1\":{\"id\":\"1\",\"name\":\"White\"}}");

        var colors = await client.GetColorListAsync(apiKey: ApiKey);

        Assert.Equal("White", Assert.Single(colors).Name);
    }
}
