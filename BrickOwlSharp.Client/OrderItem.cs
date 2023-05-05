using BrickOwlSharp.Client.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.Json.Serialization;

namespace BrickOwlSharp.Client
{
    public class OrderItem
    {
        [JsonPropertyName("image_small")]
        public string ImageSmall { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("color_name")]
        public string Color_Name { get; set; }

        [JsonPropertyName("color_id"), JsonConverter(typeof(IntStringConverter))]
        public int ColorId { get; set; }

        [JsonPropertyName("boid")]
        public string Id { get; set; }

        [JsonPropertyName("lot_id"), JsonConverter(typeof(IntStringConverter))]
        public int LotId { get; set; }

        [JsonPropertyName("condition")]
        public string Condition { get; set; }

        [JsonPropertyName("full_con")]
        public string FullCondition { get; set; }

        [JsonPropertyName("ordered_quantity"), JsonConverter(typeof(IntStringConverter))]
        public int OrderedQuantity { get; set; }

        [JsonPropertyName("personal_note")]
        public string PersonalNote { get; set; }

        [JsonPropertyName("public_note")]
        public string PublicNote { get; set; }

        [JsonPropertyName("bl_lot_id"), JsonConverter(typeof(IntStringConverter))]
        public int BlLotId { get; set; }

        /** @todo external_lot_ids
         */

        [JsonPropertyName("remaining_quantity"), JsonConverter(typeof(IntStringConverter))]
        public int RemainingQuantity { get; set; }

        [JsonPropertyName("weight"), JsonConverter(typeof(DecimalStringConverter))]
        public decimal Weight { get; set; }

        [JsonPropertyName("order_item_id"), JsonConverter(typeof(IntStringConverter))]
        public int OrderItemId { get; set; }

        [JsonPropertyName("base_price"), JsonConverter(typeof(DecimalStringConverter))]
        public decimal BasePrice { get; set; }
    }
}
