#region License
// Copyright (c) 2024 Stephan Stapel
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
# endregion
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

        [JsonPropertyName("type"), JsonConverter(typeof(ItemTypeStringConverter))]
        public ItemType Type { get; set; }

        [JsonPropertyName("tier_price"), JsonConverter(typeof(TierPriceListConverter))]
        public List<TierPrice> TierPrices { get; set; }
    }
}
