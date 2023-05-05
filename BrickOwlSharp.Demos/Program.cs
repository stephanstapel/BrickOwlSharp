using BrickOwlSharp.Client;
using BrickOwlSharp.Demos;

internal static class Program
{
    static async Task<int> Main()
    {
        BrickOwlClientConfiguration.Instance.ApiKey = "9c61000326a7ff76fc79becb739e7431d1d23586f7841db68d4a681f05c164ec";

        /*
        WishlistDemo demo = new WishlistDemo();
        demo.Run();
        */

        InventoryDemo demo = new InventoryDemo();
        await demo.RunAsync();
        return 0;
    }
}