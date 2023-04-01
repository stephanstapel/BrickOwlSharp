using BrickOwlSharp.Client;
using BrickOwlSharp.Demos;

internal class Program
{
    private static void Main(string[] args)
    {
        BrickOwlClientConfiguration.Instance.ApiKey = "<YOUR API KEY>";

        WishlistDemo demo = new WishlistDemo();
        demo.Run();
    }
}