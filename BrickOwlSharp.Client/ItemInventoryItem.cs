using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using BrickOwlSharp.Client.Json;

namespace BrickOwlSharp.Client
{
    public class ItemInventoryItem
    {
        [JsonPropertyName("boid")]
        public string Id { get; set; }

        [JsonPropertyName("quantity"), JsonConverter(typeof(IntStringConverter))]
        public int Quantity { get; set; }

        [JsonPropertyName("extra_quantity"), JsonConverter(typeof(IntStringConverter))]
        public int ExtraQuantity { get; set; }

        [JsonPropertyName("sequence_id"), JsonConverter(typeof(IntStringConverter))]
        public int? SequenceId { get; set; }

        [JsonPropertyName("alt_link")]
        public int AltLink { get; set; }
    }
}
