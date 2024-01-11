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
using System.Text;
using System.Text.Json.Serialization;

namespace BrickOwlSharp.Client
{
    [Serializable]
    public partial class CatalogItem
    {
        [JsonPropertyName("boid")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type"), JsonConverter(typeof(ItemTypeStringConverter))]
        public ItemType Type { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("permalink")]
        public string Permalink { get; set; }

        [JsonPropertyName("missing_data")]
        public string MissingData { get; set; }

        [JsonPropertyName("cat_name_path")]
        public string CatalogNamePath { get; set; }

        [JsonPropertyName("cheapest_gbp"), JsonConverter(typeof(DecimalStringConverter))]
        public decimal CheapestGBP { get; set; }

        [JsonPropertyName("color_name")]
        public string ColorName { get; set; }
        
        [JsonPropertyName("color_id"), JsonConverter(typeof(IntStringConverter))]
        public int ColorId { get; set; }        

        [JsonPropertyName("color_hex")]
        public string ColorHex { get; set; }

        [JsonPropertyName("images")]
        public List<CatalogItemImage> Images { get; set; }

        [JsonPropertyName("ids")]
        public List<CatalogItemId> Ids { get; set; }      
    }
}
