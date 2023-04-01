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
    public class UpdateInventory
    {
        [JsonPropertyName("external_id")]
        public int? ExternalId { get; set; }

        [JsonPropertyName("lot_id")]
        public int? LotId { get; set; }

        [JsonPropertyName("absolute_quantity")]
        public int? AbsoluteQuantity { get; set; }

        [JsonPropertyName("relative_quantity")]
        public int? RelativeQuantity { get; set; }

        [JsonPropertyName("for_sale")]
        public bool? ForSale { get; set; }

        [JsonPropertyName("price")]
        public decimal? Price { get; set; }

        [JsonPropertyName("sales_percent")]
        public decimal? SalesPercent { get; set; }

        [JsonPropertyName("my_cost")]
        public decimal? MyCost { get; set; }

        [JsonPropertyName("lot_weight")]
        public decimal? LotWeight { get; set; }

        [JsonPropertyName("personal_note")]
        public string PersonalNote { get; set; }

        [JsonPropertyName("public_note")]
        public string PublicNote { get; set; }

        [JsonPropertyName("bulk_qty")]
        public int? BulkQuantity { get; set; }

        [JsonPropertyName("condition"), JsonConverter(typeof(ConditionStringConverter))]
        public Condition? Condition { get; set; }

        [JsonPropertyName("update_external_id_1")]
        public int? UpdateExternalId { get; set; }

    }
}
