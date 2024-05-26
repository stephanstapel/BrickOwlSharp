using BrickOwlSharp.Client.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BrickOwlSharp.Client
{
    public class Reference
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("type"), JsonConverter(typeof(IdTypesStringConverter))]
        public IdType Type { get; set; }
    }
}
