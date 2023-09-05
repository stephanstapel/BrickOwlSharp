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
using BrickOwlSharp.Demos;

internal static class Program
{
    static async Task<int> Main()
    {
        BrickOwlClientConfiguration.Instance.ApiKey = "5b855cb9aeaf2122398e71ec3139993688f0f74a3988247de6d2109667d878ab";
        BrickOwlClientConfiguration.Instance.ApiCallEvent += new BrickOwlApiCallDelegate(() =>
        {
            Console.WriteLine($"API called");
        });

        /*
        WishlistDemo demo = new WishlistDemo();
        demo.Run();
        */

        /*
        InventoryDemo demo = new InventoryDemo();
        await demo.RunAsync();
        */

        CatalogDemo catalogDemo = new CatalogDemo();
        catalogDemo.Run();

        return 0;
    }
}
