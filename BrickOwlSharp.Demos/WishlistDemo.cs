using BrickOwlSharp.Client;


namespace BrickOwlSharp.Demos
{
    internal class WishlistDemo
    {
        internal void Run()
        {
            IBrickOwlClient client = BrickOwlClientFactory.Build();
          
            Task<List<Wishlist>> wishlists = client.GetWishlistsAsync();
            wishlists.Wait();

            foreach(Wishlist wishlist in wishlists.Result) 
            {
                Console.WriteLine($"{wishlist.Id}: {wishlist.Name}");
            }

            Task.WaitAll();
        }
    }
}
