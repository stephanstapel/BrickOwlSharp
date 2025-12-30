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
using System.Text.Json;
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

        /// <summary>
        /// Batch up to 50 requests into a single API call to reduce overhead.
        /// </summary>
        /// <param name="requestsJson">
        /// JSON payload describing the batch requests, for example:
        /// {"requests":[{"endpoint":"catalog/search","request_method":"GET","params":[{"query":"Vendor"}]}]}
        /// </param>
        /// <param name="cancellationToken"></param>
        /// <returns>Raw JSON response from the API.</returns>
        public Task<JsonElement> BulkBatchAsync(string requestsJson, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve bulk catalog dumps for a specific bulk type.
        /// </summary>
        /// <param name="type">Bulk type identifier.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Raw JSON response from the API.</returns>
        public Task<JsonElement> CatalogBulkAsync(string type, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve details about multiple catalog items at once.
        /// </summary>
        /// <param name="boids">A comma-separated list of BOIDs (maximum 100).</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Raw JSON response from the API.</returns>
        public Task<JsonElement> CatalogBulkLookupAsync(IEnumerable<string> boids, CancellationToken cancellationToken = default);

        /// <summary>
        /// Search, browse, and filter the catalog.
        /// </summary>
        /// <param name="query">Search term (use "All" to browse).</param>
        /// <param name="page">Optional page number.</param>
        /// <param name="missingData">Optional missing data filter.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Raw JSON response from the API.</returns>
        public Task<JsonElement> CatalogSearchAsync(string query, int? page = null, string missingData = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve a list of lot conditions supported by the catalog.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Raw JSON response from the API.</returns>
        public Task<JsonElement> GetCatalogConditionListAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve a list of options for a given catalog field.
        /// </summary>
        /// <param name="type">Catalog field type (e.g. category_0, eye_color).</param>
        /// <param name="language">Optional language code.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Raw JSON response from the API.</returns>
        public Task<JsonElement> GetCatalogFieldOptionListAsync(string type, string language = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Create a basic catalog cart and return pricing information.
        /// </summary>
        /// <param name="itemsJson">
        /// JSON payload in the format {"items":[{"design_id":"3034","color_id":21,"qty":"1"}]}.
        /// </param>
        /// <param name="condition">Minimum condition code for items.</param>
        /// <param name="country">2-letter destination country code.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Raw JSON response from the API.</returns>
        public Task<JsonElement> CreateCatalogCartBasicAsync(string itemsJson, string condition, string country, CancellationToken cancellationToken = default);

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

        /// <summary>
        /// Update the seller note on an order.
        /// </summary>
        /// <param name="orderId">Order ID.</param>
        /// <param name="note">Seller note text.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>True if the update succeeded.</returns>
        public Task<bool> UpdateOrderNoteAsync(int orderId, string note, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve tax schemes that can be applied to orders.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Raw JSON response from the API.</returns>
        public Task<JsonElement> GetOrderTaxSchemesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Register or remove an IP address for order notifications.
        /// </summary>
        /// <param name="ipAddress">
        /// IP address to notify. Pass an empty string to remove the notification.
        /// </param>
        /// <param name="cancellationToken"></param>
        /// <returns>True if the update succeeded.</returns>
        public Task<bool> SetOrderNotifyAsync(string ipAddress, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve details for the user associated with the API key.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Raw JSON response from the API.</returns>
        public Task<JsonElement> GetUserDetailsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve the addresses associated with the user account.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Raw JSON response from the API.</returns>
        public Task<JsonElement> GetUserAddressesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve details for the API key owner (deprecated endpoint).
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Raw JSON response from the API.</returns>
        public Task<JsonElement> GetTokenDetailsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve invoice transactions.
        /// </summary>
        /// <param name="invoiceId">Invoice identifier.</param>
        /// <param name="idType">Invoice ID type (e.g. public_invoice_id or stripe_charge_id).</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Raw JSON response from the API.</returns>
        public Task<JsonElement> GetInvoiceTransactionsAsync(string invoiceId, string idType, CancellationToken cancellationToken = default);

        public Task<bool> LeaveFeedbackAsync(int orderId , FeedbackRating rating, string comment = null, CancellationToken cancellationToken = default);
    }       
}
