using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BrickOwlSharp.Client
{
    internal class ItemInventoryItemCollection
    {
        [JsonPropertyName("inventory")]
        public List<ItemInventoryItem> Items { get; set; } = new List<ItemInventoryItem>();
    }
}
