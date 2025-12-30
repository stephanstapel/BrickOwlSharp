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
using Spectre.Console;

/// <summary>
/// Demonstrates order endpoints with sample read and feedback calls.
/// </summary>
internal class OrderDemo
{

    /// <summary>
    /// Runs order samples against the BrickOwl API.
    /// </summary>
    internal async Task RunAsync()
    {
        IBrickOwlClient client = BrickOwlClientFactory.Build();

        // Toggle feedback samples to avoid accidental feedback submissions.
        bool runFeedbackSample = false;

        if (runFeedbackSample)
        {
            // Sample: leave feedback for an order.
            bool result = await client.LeaveFeedbackAsync(0, FeedbackRating.Positive, "Great buyer, fast payment!");
            Console.WriteLine($"Feedback submitted: {result}");
        }

        // Sample: list orders sorted by updated date.
        List<BrickOwlSharp.Client.Order> allOrders = await client.GetOrdersAsync(orderSortType: OrderSortType.Updated);

        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Date");
        table.AddColumn("State");

        foreach (BrickOwlSharp.Client.Order order in allOrders)
        {
            table.AddRow(order.Id.ToString(), order.OrderDate.ToShortDateString(), order.Status);
        }

        AnsiConsole.Write(table);
    }
}
