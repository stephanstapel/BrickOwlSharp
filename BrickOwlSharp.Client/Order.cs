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
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace BrickOwlSharp.Client
{
    public class Order
    {
        [JsonPropertyName("order_id"), JsonConverter(typeof(IntStringConverter))]
        public int Id { get; set; }

        [JsonPropertyName("order_date"), JsonConverter(typeof(DateTimeStringConverter))]
        public DateTime OrderDate { get; set; }

        [JsonPropertyName("total_quantity"), JsonConverter(typeof(IntStringConverter))]
        public int TotalQuantity { get; set; }

        [JsonPropertyName("total_lots"), JsonConverter(typeof(IntStringConverter))]
        public int TotalLots { get; set; }

        [JsonPropertyName("base_order_total"), JsonConverter(typeof(DecimalStringConverter))]
        public decimal BaseOrderTotal { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("status_id"), JsonConverter(typeof(IntStringConverter))]
        public int StatusId { get; set; }
    }
}