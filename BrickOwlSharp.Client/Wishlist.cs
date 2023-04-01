using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BrickOwlSharp.Client
{
    public class Wishlist
    {
        [JsonPropertyName("wishlist_id")]
        public string Id { get; set; }


        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
