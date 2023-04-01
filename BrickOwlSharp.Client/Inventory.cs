using BrickOwlSharp.Client.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BrickOwlSharp.Client
{
    public class Inventory
    {
        [JsonPropertyName("boid")]
        public string Id { get; set; }

        [JsonPropertyName("con"), JsonConverter(typeof(ConditionStringConverter))]
        public Condition Condition { get; set; }

        [JsonPropertyName("full_con"), JsonConverter(typeof(ConditionStringConverter))]
        public Condition FullCondition { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("qty"), JsonConverter(typeof(IntStringConverter))]
        public int? Quantity { get; set; }

        [JsonPropertyName("lot_id"), JsonConverter(typeof(IntStringConverter))]
        public int? LotId { get; set; }

        [JsonPropertyName("price"), JsonConverter(typeof(DecimalStringConverter))]
        public decimal? Price { get; set; }

        [JsonPropertyName("base_price"), JsonConverter(typeof(DecimalStringConverter))]
        public decimal? BasePrice { get; set; }

        [JsonPropertyName("final_price"), JsonConverter(typeof(DecimalStringConverter))]
        public decimal? FinalPrice { get; set; }

        [JsonPropertyName("owl_id"), JsonConverter(typeof(IntStringConverter))]
        public int? OwlId { get; set; }

        [JsonPropertyName("public_note")]
        public string PublicNote{ get; set; }

        [JsonPropertyName("personal_note")]
        public string PersonalNote { get; set; }

        [JsonPropertyName("sale_percent"), JsonConverter(typeof(DecimalStringConverter))]
        public decimal? SalePercent { get; set; }

        [JsonPropertyName("bulk_qty"), JsonConverter(typeof(IntStringConverter))]
        public int? BulkQuantity { get; set; }

        [JsonPropertyName("for_sale"), JsonConverter(typeof(BoolStringConverter))]
        public bool? ForSale { get; set; }

        [JsonPropertyName("my_cost"), JsonConverter(typeof(DecimalStringConverter))]
        public decimal? MyCost { get; set; }

        [JsonPropertyName("lot_weight"), JsonConverter(typeof(DecimalStringConverter))]
        public decimal? LotWeight { get; set; }

        [JsonPropertyName("reserve_uid"), JsonConverter(typeof(IntStringConverter))]
        public int? ReserveUid { get; set; }

        [JsonPropertyName("type")]
        public string Type{ get; set; }
    }
}
