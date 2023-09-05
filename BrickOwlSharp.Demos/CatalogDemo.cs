#region License
// Copyright (c) 2023 Stephan Stapel
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
        internal void Run()
        {
            IBrickOwlClient client = BrickOwlClientFactory.Build();
           
            Task<List<string>> boids = client.CatalogIdLookupAsync("3005", ItemType.Part, IdType.DesignId); // brick 1x1
            boids.Wait();

            // retrieve item availability
            Task<Dictionary<string, CatalogItemAvailability>> availability = client.CatalogAvailabilityAsync("737117-39", "DE");
            availability.Wait();            


            // retrieve a single catalog item
            Task<CatalogItem> item = client.CatalogLookupAsync("737117-39");
            item.Wait();
            
          
            // retrieve the entire catalog
            Task<List<CatalogItem>> catalog = client.GetCatalogAsync();
            catalog.Wait();

            foreach(CatalogItem catalogItem in catalog.Result) 
            {
                Console.WriteLine($"{catalogItem.Id}: {catalogItem.Name}");
            }
            

            Task.WaitAll();          
        }
    }
}
