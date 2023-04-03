# BrickOwlSharp

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

## Wishlist
To receive the currently existing wishlists:
```C#
Task<List<Wishlist>> wishlists = await client.GetWishlistsAsync();

foreach(Wishlist wishlist in wishlists.Result) 
{
  Console.WriteLine($"{wishlist.Id}: {wishlist.Name}");
}
```


## Further functionality
If you should have further use cases you'd like to implement, drop an issue or - even better - drop a pull request.
