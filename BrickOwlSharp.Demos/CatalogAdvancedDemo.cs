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
using System.Linq;
using System.Text.Json;

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
            JsonElement bulkCatalog = await client.CatalogBulkAsync("catalog");
            Console.WriteLine($"Bulk catalog payload has properties: {bulkCatalog.EnumerateObject().Count()}");

            // Sample: bulk lookup for a list of BOIDs.
            JsonElement bulkLookup = await client.CatalogBulkLookupAsync(new[] { "737117-39", "1067768" });
            Console.WriteLine($"Bulk lookup payload has properties: {bulkLookup.EnumerateObject().Count()}");

            // Sample: catalog search for a query.
            JsonElement searchResults = await client.CatalogSearchAsync("Brick", page: 1);
            Console.WriteLine($"Catalog search payload has properties: {searchResults.EnumerateObject().Count()}");

            // Sample: list catalog conditions.
            JsonElement conditions = await client.GetCatalogConditionListAsync();
            Console.WriteLine($"Catalog conditions payload has properties: {conditions.EnumerateObject().Count()}");

            // Sample: list field options for a catalog attribute.
            JsonElement fieldOptions = await client.GetCatalogFieldOptionListAsync("category_0", "en");
            Console.WriteLine($"Catalog field options payload has properties: {fieldOptions.EnumerateObject().Count()}");

            // Sample: create a basic catalog cart for pricing.
            string itemsJson = "{\"items\":[{\"design_id\":\"3034\",\"color_id\":21,\"qty\":\"1\"}]}";
            JsonElement cart = await client.CreateCatalogCartBasicAsync(itemsJson, "N", "US");
            Console.WriteLine($"Catalog cart payload has properties: {cart.EnumerateObject().Count()}");

            // Sample: batch multiple requests into a single API call.
            string batchJson = "{\"requests\":[{\"endpoint\":\"catalog/search\",\"request_method\":\"GET\",\"params\":[{\"query\":\"Vendor\"}]}]}";
            JsonElement batchResponse = await client.BulkBatchAsync(batchJson);
            Console.WriteLine($"Batch response payload has properties: {batchResponse.EnumerateObject().Count()}");
        }
    }
}
