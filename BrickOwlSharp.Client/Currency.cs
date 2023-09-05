using System;
using System.Collections.Generic;
using System.Text;

namespace BrickOwlSharp.Client
{
    public enum Currency
    {
        Unknown,
        AUD, 
        CAD, 
        CZK, 
        DKK, 
        EUR, 
        GBP, 
        HUF, 
        NOK, 
        PLN, 
        SEK, 
        SGD, 
        THB, 
        USD
    }


    internal static class CurrencyExtensions
    {
        public static Currency FromString(this Currency _, string s)
        {
            try
            {
                return (Currency)Enum.Parse(typeof(Currency), s);
            }
            catch
            {
                return Currency.Unknown;
            }
        } // !FromString()


        public static string EnumToString(this Currency c)
        {
            return c.ToString("g");
        } // !EnumToString()
    }
}
