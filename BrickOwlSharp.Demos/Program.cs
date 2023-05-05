using BrickOwlSharp.Client;
using BrickOwlSharp.Demos;

internal static class Program
{
    static async Task<int> Main()
    {
        BrickOwlClientConfiguration.Instance.ApiKey = "----";

        /*
        WishlistDemo demo = new WishlistDemo();
        demo.Run();
        */

        InventoryDemo demo = new InventoryDemo();
        await demo.RunAsync();
        return 0;
    }
}
