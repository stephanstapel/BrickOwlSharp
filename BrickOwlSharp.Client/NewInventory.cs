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
    public class NewInventory
    {
        [JsonPropertyName("boid")]
        public int Id { get; set; }

        [JsonPropertyName("color_id")]
        public int? ColorId { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("condition"), JsonConverter(typeof(ConditionStringConverter))]
        public Condition Condition { get; set; }

        [JsonPropertyName("external_id")]
        public string ExternalId { get; set; }
    }
}
