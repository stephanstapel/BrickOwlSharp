using BrickOwlSharp.Client.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BrickOwlSharp.Client
{
    public class DeleteInventory
    {
        [JsonPropertyName("lot_id")]
        public int LotId { get; set; }

        [JsonPropertyName("external_id")]
        public string ExternalId { get; set; }
    }
}
