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
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BrickOwlSharp.Client;
using Xunit;

namespace BrickOwlSharp.Tests;

/// <summary>
/// BrickOwl always uses '.' as the decimal separator, so parsing and formatting must not depend on
/// the culture of the machine the library runs on.
/// </summary>
public class InvariantCultureTests
{
    private const string ApiKey = "secret-api-key";

    private static async Task RunWithCultureAsync(string cultureName, Func<Task> action)
    {
        CultureInfo previousCulture = CultureInfo.CurrentCulture;
        try
        {
            CultureInfo.CurrentCulture = new CultureInfo(cultureName);
            await action();
        }
        finally
        {
            CultureInfo.CurrentCulture = previousCulture;
        }
    }

    [Theory]
    [InlineData("pl-PL")]
    [InlineData("de-DE")]
    public Task TierPriceListConverter_Read_UsesInvariantCulture(string cultureName)
    {
        return RunWithCultureAsync(cultureName, () =>
        {
            var inventory = JsonSerializer.Deserialize<Inventory>("{\"tier_price\":[[\"10\",\"0.050\"],[\"50\",\"1.5\"]]}");

            Assert.Collection(inventory!.TierPrices,
                tier =>
                {
                    Assert.Equal(10, tier.Quantity);
                    Assert.Equal(0.050m, tier.Price);
                },
                tier =>
                {
                    Assert.Equal(50, tier.Quantity);
                    Assert.Equal(1.5m, tier.Price);
                });
            return Task.CompletedTask;
        });
    }

    [Fact]
    public Task GetInventoryAsync_WithTierPricesOnPolishSystem_IsParsed()
    {
        return RunWithCultureAsync("pl-PL", async () =>
        {
            const string body = "[{\"lot_id\":\"1\",\"price\":\"1.54\",\"tier_price\":[[\"10\",\"0.050\"]]}]";
            using var client = BrickOwlClientFactory.Build(new HttpClient(StubHttpMessageHandler.Returning(HttpStatusCode.OK, body)));

            var inventory = await client.GetInventoryAsync(apiKey: ApiKey);

            Inventory lot = Assert.Single(inventory);
            Assert.Equal(1.54m, lot.Price);
            Assert.Equal(0.050m, Assert.Single(lot.TierPrices).Price);
        });
    }

    [Fact]
    public Task UpdateInventoryAsync_OnPolishSystem_SendsPriceWithDecimalPoint()
    {
        return RunWithCultureAsync("pl-PL", async () =>
        {
            string? capturedBody = null;
            var handler = new StubHttpMessageHandler(async (request, _) =>
            {
                capturedBody = await request.Content!.ReadAsStringAsync();
                return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("{\"status\":\"success\"}") };
            });
            using var client = BrickOwlClientFactory.Build(new HttpClient(handler));

            await client.UpdateInventoryAsync(new UpdateInventory { LotId = 1, Price = 1.54m }, apiKey: ApiKey);

            Assert.Contains("price=1.54", capturedBody);
        });
    }

    [Fact]
    public Task NullableDateTimeStringConverter_Read_ParsesIsoDateOnPolishSystem()
    {
        return RunWithCultureAsync("pl-PL", () =>
        {
            var details = JsonSerializer.Deserialize<OrderDetails>("{\"iso_order_time\":\"2024-01-15T12:00:00+00:00\"}");

            Assert.Equal(new DateTime(2024, 1, 15, 12, 0, 0, DateTimeKind.Utc), details!.IsoOrderTime!.Value.ToUniversalTime());
            return Task.CompletedTask;
        });
    }
}
