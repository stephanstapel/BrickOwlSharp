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
