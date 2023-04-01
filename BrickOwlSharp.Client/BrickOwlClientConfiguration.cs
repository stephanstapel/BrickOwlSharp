using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickOwlSharp.Client
{
    public class BrickOwlClientConfiguration
    {
        public string ApiKey { get; set; }

        private static BrickOwlClientConfiguration _instance;
        public static BrickOwlClientConfiguration Instance
        {
            get
            {
                if (BrickOwlClientConfiguration._instance == null)
                {
                    BrickOwlClientConfiguration._instance = new BrickOwlClientConfiguration();
                }
                return _instance;
            }
        }


        private BrickOwlClientConfiguration()
        {
        }

        internal void ValidateThrowException()
        {

        }
    }
}
