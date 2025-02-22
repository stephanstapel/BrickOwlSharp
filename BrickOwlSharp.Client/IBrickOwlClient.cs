﻿#region License
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
        Task<List<Order>> GetOrdersAsync(
            OrderStatus? orderStatusFilter = null,
            DateTime? minOrderTime = null,
            int? limit = null,
            OrderType? orderType = null,
            OrderSortType? orderSortType = null,
            CancellationToken cancellationToken = default);
        Task<OrderDetails> GetOrderAsync(int orderId, CancellationToken cancellationToken = default);
        Task<bool> UpdateOrderStatusAsync(int orderId, OrderStatus status, CancellationToken cancellationToken = default);
        Task<bool> UpdateOrderTrackingAsync(int orderId, string trackingIdOrUrl, CancellationToken cancellationToken = default);
        Task<List<Wishlist>> GetWishlistsAsync(CancellationToken cancellationToken = default);
        Task<List<CatalogItem>> GetCatalogAsync(CancellationToken cancellationToken = default);

        Task<Dictionary<string, CatalogItemAvailability>> CatalogAvailabilityAsync(string boid, string country, int? quantity = null, CancellationToken cancellationToken = default);
        Task<CatalogItem> CatalogLookupAsync(string boid, CancellationToken cancellationToken = default);
        Task<List<string>> CatalogIdLookupAsync(string boid, ItemType type, IdType? idType = null, CancellationToken cancellationToken = default);
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

        Task<List<Color>> GetColorListAsyn(CancellationToken cancellationToken = default);
    }       
}
