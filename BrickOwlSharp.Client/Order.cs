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