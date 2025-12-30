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
    /// Demonstrates user and token detail endpoints.
    /// </summary>
    internal class UserDemo
    {
        /// <summary>
        /// Runs user detail samples against the BrickOwl API.
        /// </summary>
        internal async Task RunAsync()
        {
            IBrickOwlClient client = BrickOwlClientFactory.Build();

            // Sample: retrieve user account details.
            JsonElement userDetails = await client.GetUserDetailsAsync();
            Console.WriteLine($"User details payload has properties: {userDetails.EnumerateObject().Count()}");

            // Sample: retrieve user addresses.
            JsonElement userAddresses = await client.GetUserAddressesAsync();
            Console.WriteLine($"User addresses payload has properties: {userAddresses.EnumerateObject().Count()}");

            // Sample: retrieve deprecated token details.
            JsonElement tokenDetails = await client.GetTokenDetailsAsync();
            Console.WriteLine($"Token details payload has properties: {tokenDetails.EnumerateObject().Count()}");
        }
    }
}
