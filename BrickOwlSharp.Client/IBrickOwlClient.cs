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
    /// <summary>
    /// Client abstraction for interacting with the BrickOwl API.
    /// </summary>
    /// <example>
    /// <code>
    /// BrickOwlClientConfiguration.Instance.ApiKey = "<your api key>";
    ///
    /// IBrickOwlClient client = BrickOwlClientFactory.Build();
    /// List&lt;Order&gt; allOrders = await client.GetOrdersAsync(orderSortType: OrderSortType.Updated);
    /// </code>
    /// </example>
    public interface IBrickOwlClient
    {
        /// <summary>
        /// Retrieve a list of orders for the authenticated account.
        /// </summary>
        /// <param name="orderStatusFilter">Optional status filter.</param>
        /// <param name="minOrderTime">Optional minimum order time (UTC).</param>
        /// <param name="limit">Optional limit for the number of orders returned.</param>
        /// <param name="orderType">Optional order list type (store vs customer).</param>
        /// <param name="orderSortType">Optional sort order.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>List of orders.</returns>
        public Task<List<Order>> GetOrdersAsync(
            OrderStatus? orderStatusFilter = null,
            DateTime? minOrderTime = null,
            int? limit = null,
            OrderType? orderType = null,
            OrderSortType? orderSortType = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve full order details, including items.
        /// </summary>
        /// <param name="orderId">Order ID.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>Order details and item list.</returns>
        public Task<OrderDetails> GetOrderAsync(int orderId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Change the status of an order.
        /// </summary>
        /// <param name="orderId">Order ID.</param>
        /// <param name="status">New status.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>True if the update succeeded.</returns>
        public Task<bool> UpdateOrderStatusAsync(int orderId, OrderStatus status, CancellationToken cancellationToken = default);

        /// <summary>
        /// Attach tracking information to an order.
        /// </summary>
        /// <param name="orderId">Order ID.</param>
        /// <param name="trackingIdOrUrl">Tracking ID or URL.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>True if the update succeeded.</returns>
        public Task<bool> UpdateOrderTrackingAsync(int orderId, string trackingIdOrUrl, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve the wishlists available on the account.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>List of wishlists.</returns>
        public Task<List<Wishlist>> GetWishlistsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve the full catalog list.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>List of catalog items.</returns>
        public Task<List<CatalogItem>> GetCatalogAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Batch up to 50 requests into a single API call to reduce overhead.
        /// </summary>
        /// <param name="requests">
        /// Batch request definitions as tuples of endpoint, request method, and parameter key/value pairs.
        /// </param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>Batch response details.</returns>
        public Task<BulkBatchResponse> BulkBatchAsync(
            IEnumerable<(string Endpoint, string RequestMethod, IEnumerable<Dictionary<string, string>> Parameters)> requests,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve bulk catalog dumps for a specific bulk type.
        /// </summary>
        /// <param name="type">Bulk type identifier.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>Bulk catalog response details.</returns>
        public Task<CatalogBulkResponse> CatalogBulkAsync(string type, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve details about multiple catalog items at once.
        /// </summary>
        /// <param name="boids">A comma-separated list of BOIDs (maximum 100).</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>Catalog bulk lookup response details.</returns>
        public Task<CatalogBulkLookupResponse> CatalogBulkLookupAsync(IEnumerable<string> boids, CancellationToken cancellationToken = default);

        /// <summary>
        /// Search, browse, and filter the catalog.
        /// </summary>
        /// <param name="query">Search term (use "All" to browse).</param>
        /// <param name="page">Optional page number.</param>
        /// <param name="missingData">Optional missing data filter.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>Catalog search response details.</returns>
        public Task<CatalogSearchResponse> CatalogSearchAsync(string query, int? page = null, string missingData = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve a list of lot conditions supported by the catalog.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>Catalog condition list response details.</returns>
        public Task<CatalogConditionListResponse> GetCatalogConditionListAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve a list of options for a given catalog field.
        /// </summary>
        /// <param name="type">Catalog field type (e.g. category_0, eye_color).</param>
        /// <param name="language">Optional language code.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>Catalog field option list response details.</returns>
        public Task<CatalogFieldOptionListResponse> GetCatalogFieldOptionListAsync(string type, string language = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Create a basic catalog cart and return pricing information.
        /// </summary>
        /// <param name="items">Catalog cart items as tuples of design ID, color ID, BOID, and quantity.</param>
        /// <param name="condition">Minimum condition code for items.</param>
        /// <param name="country">2-letter destination country code.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>Cart pricing details.</returns>
        public Task<CatalogCartBasicResponse> CreateCatalogCartBasicAsync(
            IEnumerable<(string DesignId, int? ColorId, string Boid, int Quantity)> items,
            string condition,
            string country,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve pricing and availability for a catalog item.
        /// </summary>
        /// <param name="boid">BOID of the item.</param>
        /// <param name="country">2-letter destination country code.</param>
        /// <param name="quantity">Optional minimum quantity.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>Availability information keyed by store ID.</returns>
        public Task<Dictionary<string, CatalogItemAvailability>> CatalogAvailabilityAsync(string boid, string country, int? quantity = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve catalog details for a single BOID.
        /// </summary>
        /// <param name="boid">BOID of the item.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>Catalog item details.</returns>
        public Task<CatalogItem> CatalogLookupAsync(string boid, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve possible BOIDs for a given external ID.
        /// </summary>
        /// <param name="boid">External ID value.</param>
        /// <param name="type">Item type filter.</param>
        /// <param name="idType">Optional ID type filter.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>List of matching BOIDs.</returns>
        public Task<List<string>> CatalogIdLookupAsync(string boid, ItemType type, IdType? idType = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Create a new lot in store inventory.
        /// </summary>
        /// <param name="newInventory">New inventory details.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>Result containing the new lot ID.</returns>
        public Task<NewInventoryResult> CreateInventoryAsync(NewInventory newInventory, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update an existing inventory lot.
        /// </summary>
        /// <param name="updatedInventory">Inventory update payload.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>True if the update succeeded.</returns>
        public Task<bool> UpdateInventoryAsync(
            UpdateInventory updatedInventory,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve inventory lots, optionally filtered by status or identifiers.
        /// </summary>
        /// <param name="filter">Optional filter string.</param>
        /// <param name="activeOnly">Optional active-only flag.</param>
        /// <param name="externalId">Optional external ID filter.</param>
        /// <param name="lotId">Optional lot ID filter.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>List of inventory lots.</returns>
        public Task<List<Inventory>> GetInventoryAsync(
            string filter = null, bool? activeOnly = null, string externalId = null, int? lotId = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete an inventory lot.
        /// </summary>
        /// <param name="deleteInventory">Deletion payload.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>True if the deletion succeeded.</returns>
        public Task<bool> DeleteInventoryAsync(
           DeleteInventory deleteInventory,
           CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve the catalog color list.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>List of colors.</returns>
        public Task<List<Color>> GetColorListAsyn(CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns the item inventory for a given BOID. If you are passing the BOID of a set, the response
        /// will include all parts inside that set.
        /// </summary>
        /// <param name="boid">BOID of the item, e.g. the set</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>Inventory items included in the item</returns>
        public Task<List<ItemInventoryItem>> GetItemInventoryAsync(string boid, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update the seller note on an order.
        /// </summary>
        /// <param name="orderId">Order ID.</param>
        /// <param name="note">Seller note text.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>True if the update succeeded.</returns>
        public Task<bool> UpdateOrderNoteAsync(int orderId, string note, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve tax schemes that can be applied to orders.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>Raw JSON response from the API.</returns>
        public Task<OrderTaxSchemesResponse> GetOrderTaxSchemesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Register or remove an IP address for order notifications.
        /// </summary>
        /// <param name="ipAddress">
        /// IP address to notify. Pass an empty string to remove the notification.
        /// </param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>True if the update succeeded.</returns>
        public Task<bool> SetOrderNotifyAsync(string ipAddress, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve details for the user associated with the API key.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>Raw JSON response from the API.</returns>
        public Task<UserDetailsResponse> GetUserDetailsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve the addresses associated with the user account.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>Raw JSON response from the API.</returns>
        public Task<UserAddressesResponse> GetUserAddressesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve details for the API key owner (deprecated endpoint).
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>Raw JSON response from the API.</returns>
        public Task<TokenDetailsResponse> GetTokenDetailsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve invoice transactions.
        /// </summary>
        /// <param name="invoiceId">Invoice identifier.</param>
        /// <param name="idType">Invoice ID type (e.g. public_invoice_id or stripe_charge_id).</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>Raw JSON response from the API.</returns>
        public Task<InvoiceTransactionsResponse> GetInvoiceTransactionsAsync(string invoiceId, string idType, CancellationToken cancellationToken = default);

        /// <summary>
        /// Leave feedback for an order.
        /// </summary>
        /// <param name="orderId">Order ID.</param>
        /// <param name="rating">Feedback rating.</param>
        /// <param name="comment">Optional feedback comment.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>True if the feedback was accepted.</returns>
        public Task<bool> LeaveFeedbackAsync(int orderId , FeedbackRating rating, string comment = null, CancellationToken cancellationToken = default);
    }       
}
