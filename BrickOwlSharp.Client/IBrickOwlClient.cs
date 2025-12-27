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
        public Task<List<Order>> GetOrdersAsync(
            OrderStatus? orderStatusFilter = null,
            DateTime? minOrderTime = null,
            int? limit = null,
            OrderType? orderType = null,
            OrderSortType? orderSortType = null,
            CancellationToken cancellationToken = default);
        public Task<OrderDetails> GetOrderAsync(int orderId, CancellationToken cancellationToken = default);
        public Task<bool> UpdateOrderStatusAsync(int orderId, OrderStatus status, CancellationToken cancellationToken = default);
        public Task<bool> UpdateOrderTrackingAsync(int orderId, string trackingIdOrUrl, CancellationToken cancellationToken = default);
        public Task<List<Wishlist>> GetWishlistsAsync(CancellationToken cancellationToken = default);
        public Task<List<CatalogItem>> GetCatalogAsync(CancellationToken cancellationToken = default);

        public Task<Dictionary<string, CatalogItemAvailability>> CatalogAvailabilityAsync(string boid, string country, int? quantity = null, CancellationToken cancellationToken = default);
        public Task<CatalogItem> CatalogLookupAsync(string boid, CancellationToken cancellationToken = default);
        public Task<List<string>> CatalogIdLookupAsync(string boid, ItemType type, IdType? idType = null, CancellationToken cancellationToken = default);
        public Task<NewInventoryResult> CreateInventoryAsync(NewInventory newInventory, CancellationToken cancellationToken = default);
        public Task<bool> UpdateInventoryAsync(
            UpdateInventory updatedInventory,
            CancellationToken cancellationToken = default);
        public Task<List<Inventory>> GetInventoryAsync(
            string filter = null, bool? activeOnly = null, string externalId = null, int? lotId = null,
            CancellationToken cancellationToken = default);

        public Task<bool> DeleteInventoryAsync(
           DeleteInventory deleteInventory,
           CancellationToken cancellationToken = default);

        public Task<List<Color>> GetColorListAsyn(CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns the item inventory for a given BOID. If you are passing the BOID of a set, the response
        /// will include all parts inside that set.
        /// </summary>
        /// <param name="boid">BOID of the item, e.g. the set</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Inventory items included in the item</returns>
        public Task<List<ItemInventoryItem>> GetItemInventoryAsync(string boid, CancellationToken cancellationToken = default);

        public Task<bool> LeaveFeedbackAsync(int orderId , FeedbackRating rating, string comment = null, CancellationToken cancellationToken = default);
    }       
}
