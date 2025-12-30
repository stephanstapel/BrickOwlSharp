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
using BrickOwlSharp.Client;
using System.Collections.Generic;

namespace BrickOwlSharp.Demos
{
    /// <summary>
    /// Demonstrates advanced catalog endpoints such as bulk, search, and cart requests.
    /// </summary>
    internal class CatalogAdvancedDemo
    {
        /// <summary>
        /// Runs advanced catalog samples against the BrickOwl API.
        /// </summary>
        internal async Task RunAsync()
        {
            IBrickOwlClient client = BrickOwlClientFactory.Build();

            // Sample: bulk download metadata for catalog updates.
            CatalogBulkResponse bulkCatalog = await client.CatalogBulkAsync("catalog");
            Console.WriteLine($"Bulk catalog payload has properties: {bulkCatalog.AdditionalData.Count}");

            // Sample: bulk lookup for a list of BOIDs.
            CatalogBulkLookupResponse bulkLookup = await client.CatalogBulkLookupAsync(["737117-39", "1067768"]);
            Console.WriteLine($"Bulk lookup payload has properties: {bulkLookup.AdditionalData.Count}");

            // Sample: catalog search for a query.
            CatalogSearchResponse searchResults = await client.CatalogSearchAsync("Brick", page: 1);
            Console.WriteLine($"Catalog search payload has properties: {searchResults.AdditionalData.Count}");

            // Sample: list catalog conditions.
            CatalogConditionListResponse conditions = await client.GetCatalogConditionListAsync();
            Console.WriteLine($"Catalog conditions payload has properties: {conditions.AdditionalData.Count}");

            // Sample: list field options for a catalog attribute.
            CatalogFieldOptionListResponse fieldOptions = await client.GetCatalogFieldOptionListAsync("category_0", "en");
            Console.WriteLine($"Catalog field options payload has properties: {fieldOptions.AdditionalData.Count}");

            // Sample: create a basic catalog cart for pricing.
            var cartItems = new (string DesignId, int? ColorId, string? Boid, int Quantity)[]
            {
                ("3034", 21, null, 1)
            };
            CatalogCartBasicResponse cart = await client.CreateCatalogCartBasicAsync(
                cartItems,
                "N",
                "US");
            Console.WriteLine($"Catalog cart returned {cart.Items.Count} items.");

            // Sample: batch multiple requests into a single API call.
            var batchRequests = new (string Endpoint, string RequestMethod, IEnumerable<Dictionary<string, string>> Parameters)[]
            {
                ("catalog/search",
                    "GET",
                    new List<Dictionary<string, string>>
                    {
                        new() {
                            { "query", "Vendor" }
                        }
                    })
            };
            BulkBatchResponse batchResponse = await client.BulkBatchAsync(batchRequests);
            Console.WriteLine($"Batch response contained {batchResponse.Responses.Count} responses.");
        }
    }
}
