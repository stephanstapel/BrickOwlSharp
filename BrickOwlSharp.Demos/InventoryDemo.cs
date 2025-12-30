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
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickOwlSharp.Demos
{
    /// <summary>
    /// Demonstrates inventory endpoints with read and write sample cases.
    /// </summary>
    internal class InventoryDemo
    {
        /// <summary>
        /// Runs inventory samples against the BrickOwl API.
        /// </summary>
        internal async Task RunAsync()
        {
            IBrickOwlClient client = BrickOwlClientFactory.Build();

            // Toggle write samples to avoid accidental mutations.
            bool runWriteSamples = false;

            if (runWriteSamples)
            {
                // Sample: create a new inventory lot.
                NewInventoryResult newInventoryResult = await client.CreateInventoryAsync(new NewInventory()
                {
                    Id = "414759", // Bracket 1 x 2 - 1 x 2 Inverted
                    Condition = Condition.New,
                    Quantity = 1,
                    Price = 1000.15m // make sure nobody will ever buy it :)
                });

                // Sample: update the inventory lot created above.
                bool updateResult = await client.UpdateInventoryAsync(new UpdateInventory()
                {
                    LotId = newInventoryResult.LotId!.Value,
                    AbsoluteQuantity = 23
                });

                Console.WriteLine($"Inventory update result: {updateResult}");

                // Sample: delete the inventory lot created above.
                bool deleteResult = await client.DeleteInventoryAsync(new DeleteInventory()
                {
                    LotId = newInventoryResult.LotId.Value
                });

                Console.WriteLine($"Inventory delete result: {deleteResult}");
            }

            var table = new Table();
            table.AddColumn("Id");
            table.AddColumn("Lot Id");
            table.AddColumn("Quantity");
            table.AddColumn("Type");

            // Sample: list all inventory lots.
            foreach (Inventory inventory in await client.GetInventoryAsync())
            {
                table.AddRow(inventory.Id, inventory.LotId.HasValue ? inventory.LotId.Value.ToString() : "", inventory.Quantity.HasValue ? inventory.Quantity.Value.ToString() : "", inventory.Type.ToString());
            }

            AnsiConsole.Write(table);
        }
    }
}
