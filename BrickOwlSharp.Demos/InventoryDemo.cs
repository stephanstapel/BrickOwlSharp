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
        internal async Task RunAsync()
        {
            IBrickOwlClient client = BrickOwlClientFactory.Build();

            NewInventoryResult newInventoryResult = await client.CreateInventoryAsync(new NewInventory()
            {
                Id = 414759, // Bracket 1 x 2 - 1 x 2 Inverted
                Condition = Condition.New,
                Quantity = 1,
                Price = 1000.15m // make sure nobody will ever buy it :)
            });

            /*
            bool updateResult = await client.UpdateInventoryAsync(new UpdateInventory()
            {
                LotId = 107931517,
                AbsoluteQuantity = 23                
            });            
            */

            foreach (Inventory inventory in await client.GetInventoryAsync())
            {
                Console.WriteLine($"{inventory.Id}: quantity {inventory.Quantity}, lot id: {inventory.LotId}");
            }

            bool result = await client.DeleteInventoryAsync(new DeleteInventory() { LotId = 107931627 });
        }
    }
}
