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
    /// <summary>
    /// Demonstrates order management endpoints such as status, tracking, and notes.
    /// </summary>
    internal class OrderManagementDemo
    {
        /// <summary>
        /// Runs order management samples against the BrickOwl API.
        /// </summary>
        internal async Task RunAsync()
        {
            IBrickOwlClient client = BrickOwlClientFactory.Build();

            // Sample: retrieve a single order with full details.
            const int sampleOrderId = 0;
            OrderDetails orderDetails = await client.GetOrderAsync(sampleOrderId);
            Console.WriteLine($"Order {orderDetails.Id} has {orderDetails.OrderItems.Count} items.");

            // Sample: retrieve tax scheme metadata.
            OrderTaxSchemesResponse taxSchemes = await client.GetOrderTaxSchemesAsync();
            Console.WriteLine($"Tax scheme payload has properties: {taxSchemes.AdditionalData.Count}");

            // Toggle write samples to avoid accidental mutations.
            bool runOrderUpdates = false;

            if (runOrderUpdates)
            {
                bool noteUpdated = await client.UpdateOrderNoteAsync(sampleOrderId, "Packed and ready to ship.");
                Console.WriteLine($"Order note updated: {noteUpdated}");

                bool statusUpdated = await client.UpdateOrderStatusAsync(sampleOrderId, OrderStatus.Processed);
                Console.WriteLine($"Order status updated: {statusUpdated}");

                bool trackingUpdated = await client.UpdateOrderTrackingAsync(sampleOrderId, "TRACKING-123");
                Console.WriteLine($"Order tracking updated: {trackingUpdated}");

                bool notifyUpdated = await client.SetOrderNotifyAsync("203.0.113.10");
                Console.WriteLine($"Order notify IP updated: {notifyUpdated}");
            }
        }
    }
}
