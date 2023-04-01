using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BrickOwlSharp.Client
{
    public class BrickOwlResult
    {

        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
