# BrickOwlSharp
[![NuGet](https://img.shields.io/nuget/v/BrickOwlSharp.Client?color=blue)](https://www.nuget.org/packages/BrickOwlSharp.Client/1.0.0)


BrickOwlSharp is a C# library for interacting with the BrickOwl API. It provides a simple and intuitive interface for developers to access the full functionality of the BrickOwl platform, including searching for LEGO parts, sets, and minifigures, managing orders and inventory, and much more.

With BrickOwlSharp, developers can easily integrate BrickOwl into their applications and services, and build powerful tools and workflows that leverage the capabilities of the BrickOwl platform.

Whether you're building a LEGO-themed e-commerce site, a parts inventory management system, or a mobile app for LEGO enthusiasts, BrickOwlSharp can help you get up and running quickly and easily.

# License
Subject to the MIT license https://github.com/stephanstapel/BrickOwlSharp/blob/main/LICENSE

# Installation
The library is not yet available on nuget. Thus, the way to use the library is to download or fork the code and include the library BrickOwlSharp.Client in your application. It is created using .net Standard 2.0 which provides a wide range of compatibility with various .net versions.

# Usage
Currently, BrickOwlSharp does not implement the entire BrickOwl API yet.

Just like with BricklinkSharp (https://github.com/gebirgslok/BricklinkSharp) and RebrickableSharp libraries (https://github.com/gebirgslok/RebrickableSharp), the actual API requires authentication which is implemented in similar manner:

```C#
BrickOwlClientConfiguration.Instance.ApiKey = "<YOUR API KEY>";
```

If you'd like to run the demos, add an `apikey.txt` file next to `BrickOwlSharp.Demos/Program.cs` (see the demo section below) and paste your BrickOwl API key into it.

## New in this update
The following capabilities were added with the latest update and are showcased in the demo project (`BrickOwlSharp.Demos`). Each bullet links to a demo sample you can run or copy into your own code. These additions focus on account visibility, richer order workflows, and advanced catalog tooling so you can automate more of your BrickOwl processes with fewer custom API calls.

- **User and token detail endpoints** (`BrickOwlSharp.Demos/UserDemo.cs`)
  - Retrieve account metadata (profile, shop, and limits), address book entries for fulfillment, and token details (deprecated but still available in the API for legacy integrations).
- **Invoice transaction lookup** (`BrickOwlSharp.Demos/InvoiceDemo.cs`)
  - Pull transaction history for a specific invoice to reconcile payments or power a billing dashboard.
- **Order management helpers** (`BrickOwlSharp.Demos/OrderManagementDemo.cs`)
  - Read order details, inspect tax scheme metadata, and optionally update notes/status/tracking/notify IP when your workflow is ready to push changes.
- **Advanced catalog endpoints** (`BrickOwlSharp.Demos/CatalogAdvancedDemo.cs`)
  - Bulk catalog downloads for sync jobs, bulk BOID lookup for ID mapping, catalog search, attribute options for filters, cart pricing for quote engines, and batch requests to reduce round trips.
- **Color list endpoint** (`BrickOwlSharp.Demos/ColorDemo.cs`)
  - Retrieve the full BrickOwl color list to build drop-downs, validate input, or map colors in your own data model.

## Demo project quickstart
The demo project is a simple console app that you can use as a reference for writing your own integration.

1. Add your API key
   - Create `BrickOwlSharp.Demos/apikey.txt` with your BrickOwl API key.
2. Pick a demo in `BrickOwlSharp.Demos/Program.cs`
   - Uncomment the demo you want to run.
3. Run it
   - `dotnet run --project BrickOwlSharp.Demos`

### Demo: User details
`BrickOwlSharp.Demos/UserDemo.cs` calls the user detail endpoints and prints simple counts so you can validate authentication and see how payloads are returned via `AdditionalData`.

```C#
IBrickOwlClient client = BrickOwlClientFactory.Build();

// Basic account metadata (shop info, usage limits, etc.).
UserDetailsResponse userDetails = await client.GetUserDetailsAsync();
Console.WriteLine($"User details payload has properties: {userDetails.AdditionalData.Count}");

// Addresses used for billing/shipping or account settings.
UserAddressesResponse userAddresses = await client.GetUserAddressesAsync();
Console.WriteLine($"User addresses payload has properties: {userAddresses.AdditionalData.Count}");

// Legacy token details (still provided by the API for older integrations).
TokenDetailsResponse tokenDetails = await client.GetTokenDetailsAsync();
Console.WriteLine($"Token details payload has properties: {tokenDetails.AdditionalData.Count}");
```

### Demo: Invoice transactions
`BrickOwlSharp.Demos/InvoiceDemo.cs` shows how to request invoice transactions by public invoice ID. Replace `INV-000000` with your own ID to reconcile a specific invoice.

```C#
IBrickOwlClient client = BrickOwlClientFactory.Build();
// "public_invoice_id" tells BrickOwl which lookup field you are using.
InvoiceTransactionsResponse transactions = await client.GetInvoiceTransactionsAsync("INV-000000", "public_invoice_id");
Console.WriteLine($"Invoice transactions payload has properties: {transactions.AdditionalData.Count}");
```

### Demo: Order management
`BrickOwlSharp.Demos/OrderManagementDemo.cs` pulls order details and tax scheme metadata, then optionally performs mutations. Set `sampleOrderId` to a real order ID and flip `runOrderUpdates` to `true` when you are ready to write.

```C#
// Replace with a real order ID from your shop.
const int sampleOrderId = 0;
OrderDetails orderDetails = await client.GetOrderAsync(sampleOrderId);
Console.WriteLine($"Order {orderDetails.Id} has {orderDetails.OrderItems.Count} items.");

// Tax schemes are useful for reporting or custom invoice generation.
OrderTaxSchemesResponse taxSchemes = await client.GetOrderTaxSchemesAsync();
Console.WriteLine($"Tax scheme payload has properties: {taxSchemes.AdditionalData.Count}");

// Enable only when you want to mutate live order data.
bool runOrderUpdates = false;
if (runOrderUpdates)
{
    // Add or update a public order note.
    await client.UpdateOrderNoteAsync(sampleOrderId, "Packed and ready to ship.");
    // Advance the order status (e.g., Processed, Shipped).
    await client.UpdateOrderStatusAsync(sampleOrderId, OrderStatus.Processed);
    // Provide carrier tracking for the buyer.
    await client.UpdateOrderTrackingAsync(sampleOrderId, "TRACKING-123");
    // Register the IP for order notifications/webhooks.
    await client.SetOrderNotifyAsync("203.0.113.10");
}
```

### Demo: Advanced catalog endpoints
`BrickOwlSharp.Demos/CatalogAdvancedDemo.cs` aggregates the "power-user" catalog flows. You can mix and match these calls depending on your use case (sync jobs, internal search, or quote tooling).

```C#
// Bulk metadata for catalog sync jobs (downloads files, not items themselves).
CatalogBulkResponse bulkCatalog = await client.CatalogBulkAsync("catalog");
// Lookup multiple BOIDs at once to reduce round trips.
CatalogBulkLookupResponse bulkLookup = await client.CatalogBulkLookupAsync(new[] { "737117-39", "1067768" });
// Text search for items (supports pagination).
CatalogSearchResponse searchResults = await client.CatalogSearchAsync("Brick", page: 1);
// Condition list helps map new/used terminology for filters.
CatalogConditionListResponse conditions = await client.GetCatalogConditionListAsync();
// Field options supply valid values for attributes such as category.
CatalogFieldOptionListResponse fieldOptions = await client.GetCatalogFieldOptionListAsync("category_0", "en");

// Build a basic cart to estimate pricing or availability.
CatalogCartBasicResponse cart = await client.CreateCatalogCartBasicAsync(
    new[] { ("3034", 21, (string?)null, 1) },
    "N",
    "US");

// Batch multiple endpoints into one API call.
var batchRequests = new (string Endpoint, string RequestMethod, IEnumerable<Dictionary<string, string>> Parameters)[]
{
    ("catalog/search",
        "GET",
        new List<Dictionary<string, string>>
        {
            // Per-request parameter dictionary.
            new() { { "query", "Vendor" } }
        })
};
BulkBatchResponse batchResponse = await client.BulkBatchAsync(batchRequests);
```

### Demo: Color list
`BrickOwlSharp.Demos/ColorDemo.cs` shows how to download all colors for UI filters or validation logic.

```C#
// Useful for building pickers or validating incoming color data.
List<BrickOwlSharp.Client.Color> allColors = await client.GetColorListAsyn();
foreach (BrickOwlSharp.Client.Color color in allColors)
{
    Console.WriteLine($"{color.Id}: {color.Name} ({color.Hex})");
}
```

## Inventory
Add an item to the inventory:

```C#
NewInventoryResult newInventoryResult = await client.CreateInventoryAsync(new NewInventory()
{
  Id = 414759, // Bracket 1 x 2 - 1 x 2 Inverted
  Condition = Condition.New,
  Quantity = 1,
  Price = 1000.15m // make sure nobody will ever buy it :)
});
```

Update an item in the inventory:

```C#
bool updateResult = await client.UpdateInventoryAsync(new UpdateInventory()
{
  LotId = 107931517,
  AbsoluteQuantity = 23                
}); 
```
In contrary to Bricklink API, BrickOwl allows to both update the quantity relatively and also absolutely. Accordingly, the library accepts either parameter.

To get an overview of the current inventory:

```C#
foreach (Inventory inventory in await client.GetInventoryAsync())
{
  Console.WriteLine($"{inventory.Id}: quantity {inventory.Quantity}, lot id: {inventory.LotId}");
}
```

To delete an element from the inventory:

```C#
bool result = await client.DeleteInventoryAsync(new DeleteInventory() { LotId = 107931627 });
```

## Orders
To receive a list of orders, invoke:
```C#
List<Order> allOrders = await client.GetOrdersAsync();
```

This information does not include the order items or the buyer details. If you need this information, call:

```C#
OrderDetails orderDetails = await client.GetOrderAsync(12345);
```

All  fields from the actual API are mapped. In case you have a better idea how to aggregate them or how to add sub classes for better structuring, drop me a note (or a PR).

Order status can also be changed, 

```C#
bool result = await client.UpdateOrderStatusAsync(12345, OrderStatus.Processed);
```

Please note: If the status you are passing to the function has already been reached or surpassed, false is returned as a result.

You can add tracking to your order using:

```C#
bool result = await client.UpdateOrderTrackingAsync(12345, "https://mytrackinurl.carrier.com");
```

## Catalog
BrickOwl provides a wide range of endpoints for retrieving various information about its catalog.
To access these functions, BrickOwl administrators have to grant access, see https://www.brickowl.com/api_docs for more details.

To retrieve the entire catalog, currently approximately 260,000 items:
```C#
Task<List<CatalogItem>> catalog = client.GetCatalogAsync();
catalog.Wait();

foreach(CatalogItem catalogItem in catalog.Result) 
{
    Console.WriteLine($"{catalogItem.Id}: {catalogItem.Name}");
}
```

Each item in the resulting list looks like this:

![grafik](https://github.com/stephanstapel/BrickOwlSharp/assets/2912080/c4d72358-4de4-4c65-8f18-afbf49085bd9)


Instead of retrieving the entire catalog, you can lookup single items:

```C#
Task<CatalogItem> item = client.CatalogLookupAsync("737117-39");
item.Wait();
```

This function will retrieve information about the antenna part only.

To find out in which shops a particular item is available, call:

```C#
Task<Dictionary<string, CatalogItemAvailability>> availability = client.CatalogAvailabilityAsync("737117-39", "DE");
availability.Wait(); 
```
This call will return a dictionary with the shop id as the key and availability information as the value.
Each element in the dictionary looks like this:

![grafik](https://github.com/stephanstapel/BrickOwlSharp/assets/2912080/2c72340e-46b5-4995-a209-e97de8245da1)


To find out the BrickOwl ids for particular design ids, ldraw ids etc., call:

```C#
Task<List<string>> boids = client.CatalogIdLookupAsync("3005", ItemType.Part, IdType.DesignId); // brick 1x1
boids.Wait();
```

This call will return all BrickOwl ids (approximately 60 in this case).

## Wishlist
To receive the currently existing wishlists:
```C#
Task<List<Wishlist>> wishlists = await client.GetWishlistsAsync();

foreach(Wishlist wishlist in wishlists.Result) 
{
  Console.WriteLine($"{wishlist.Id}: {wishlist.Name}");
}
```

## Measuring API call rate
As the available API calls per day are limited, you can collect the API call rate using a .net event.
You can simply subscribe to the event by adding a handler to this event:

```C#
BrickOwlClientConfiguration.Instance.ApiCallEvent
```

the most simply way is:

```C#
BrickOwlClientConfiguration.Instance.ApiCallEvent += new BrickOwlApiCallDelegate(() =>
        {
            Console.WriteLine($"API called");
        });
```

In reality, you might want to increase a counter in a database or a counter in a monitoring system like Prometheus.

## Further functionality
If you should have further use cases you'd like to implement, drop an issue or - even better - drop a pull request.
