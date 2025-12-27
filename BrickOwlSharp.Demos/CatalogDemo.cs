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


namespace BrickOwlSharp.Demos
{
    internal class CatalogDemo
    {
        internal async Task RunAsync()
        {
            IBrickOwlClient client = BrickOwlClientFactory.Build();

            var itemInventoryItems = await client.GetItemInventoryAsync("1067768"); // WAll-E and Eve
           
            // lookup a single design id
            List<string> boids = await client.CatalogIdLookupAsync("3005", ItemType.Part, IdType.DesignId); // brick 1x1
            
            // retrieve item availability
            Dictionary<string, CatalogItemAvailability> availability = await client.CatalogAvailabilityAsync("737117-39", "DE");

            // retrieve a single catalog item
            CatalogItem item = await client.CatalogLookupAsync("737117-39");
            
            // retrieve the entire catalog
            List<CatalogItem> catalog = await client.GetCatalogAsync();

            foreach(CatalogItem catalogItem in catalog) 
            {
                Console.WriteLine($"{catalogItem.Id}: {catalogItem.Name}");
            }
        } // !RunAsync()
    }
}
