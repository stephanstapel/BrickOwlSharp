#region License
// Copyright (c) 2023 Stephan Stapel
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
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

namespace BrickOwlSharp.Client
{
    public partial class CatalogItemAvailability
    {
        [JsonPropertyName("con"), JsonConverter(typeof(ConditionStringConverter))]
        public Condition Condition { get; set; }

        [JsonPropertyName("lot_id"), JsonConverter(typeof(IntStringConverter))]
        public int LotId { get; set; }

        [JsonPropertyName("price"), JsonConverter(typeof(DecimalStringConverter))]
        public decimal Price { get; set; }

        [JsonPropertyName("qty"), JsonConverter(typeof(IntStringConverter))]        
        public int Quantity { get; set; }

        [JsonPropertyName("bulk_qty"), JsonConverter(typeof(IntStringConverter))]
        public int BulkQuantity { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("updated"), JsonConverter(typeof(DateTimeStringConverter))]
        public DateTime Updated { get; set; }

        [JsonPropertyName("created"), JsonConverter(typeof(DateTimeStringConverter))]
        public DateTime Created { get; set; }

        [JsonPropertyName("type"), JsonConverter(typeof(ItemTypeStringConverter))]
        public ItemType Type { get; set; }

        [JsonPropertyName("set_number")]
        public object SetNumber { get; set; }

        [JsonPropertyName("boid")]
        public string Id { get; set; }

        [JsonPropertyName("store_id"), JsonConverter(typeof(IntStringConverter))]
        public int StoreId { get; set; }

        [JsonPropertyName("store_name")]
        public string StoreName { get; set; }

        [JsonPropertyName("base_currency"), JsonConverter(typeof(CurrencyStringConverter))]
        public Currency BaseCurrency { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("square_logo_24")]
        public string SquareLogo24 { get; set; }

        [JsonPropertyName("square_logo_16")]
        public string SquareLogo16 { get; set; }

        [JsonPropertyName("store_url")]
        public string StoreUrl { get; set; }

        [JsonPropertyName("feedback_count"), JsonConverter(typeof(IntStringConverter))]
        public int FeedbackCount { get; set; }

        [JsonPropertyName("minimum_order"), JsonConverter(typeof(DecimalStringConverter))]
        public decimal MinimumOrder { get; set; }

        [JsonPropertyName("minimum_lot_average"), JsonConverter(typeof(DecimalStringConverter))]
        public decimal MinimumLotAverage { get; set; }

        [JsonPropertyName("open")]
        public bool Open { get; set; }
    }
}
