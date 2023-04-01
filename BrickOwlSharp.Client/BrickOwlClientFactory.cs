using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BrickOwlSharp.Client
{
    public static class BrickOwlClientFactory
    {
        private static IBrickOwlClient Build(HttpClient httpClient, bool disposeHttpClient)
        {
            return new BrickOwlClient(httpClient, disposeHttpClient);
        }

        public static IBrickOwlClient Build(HttpClient httpClient)
        {
            return Build(httpClient, false);
        }

        public static IBrickOwlClient Build()
        {
            return Build(new HttpClient(), true);
        }
    }
}
