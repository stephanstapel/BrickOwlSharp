using BrickOwlSharp.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickOwlSharp.Demos
{
    internal class InventoryDemo
    {
        internal void Run()
        {
            IBrickOwlClient client = BrickOwlClientFactory.Build();

            Task t = client.CreateInventoryAsync(new NewInventory()
            {
                Id = 414759, // Bracket 1 x 2 - 1 x 2 Inverted
                Condition = Condition.New,
                Quantity = 1,
                Price = 1000.15m
            });

            Task.WaitAll();
        }
    }
}
