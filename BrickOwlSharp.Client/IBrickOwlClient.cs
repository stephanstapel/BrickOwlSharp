using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BrickOwlSharp.Client
{
    public interface IBrickOwlClient
    {
        Task<List<Order>> GetOrdersAsync(CancellationToken cancellationToken = default);
        Task<List<Wishlist>> GetWishlistsAsync(CancellationToken cancellationToken = default);
        Task<NewInventoryResult> CreateInventoryAsync(NewInventory newInventory, CancellationToken cancellationToken = default);
        Task<bool> UpdateInventoryAsync(
            UpdateInventory updatedInventory,
            CancellationToken cancellationToken = default);
        Task<List<Inventory>> GetInventoryAsync(
            string filter = null, bool? activeOnly = null, string externalId = null, int? lotId = null,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteInventoryAsync(
           DeleteInventory deleteInventory,
           CancellationToken cancellationToken = default);
    }       
}
