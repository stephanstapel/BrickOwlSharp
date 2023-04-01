using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BrickOwlSharp.Client
{
    public class NewInventoryResult
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        
        [JsonPropertyName("lot_id")]
        public string LotId { get; set; }
    }
}
