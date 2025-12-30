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
    public class OrderDetails
    {
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        [JsonPropertyName("order_id"), JsonConverter(typeof(IntStringConverter))]
        public int Id { get; set; }

        [JsonPropertyName("order_time"), JsonConverter(typeof(DateTimeStringConverter))]
        public DateTime OrderTime { get; set; }

        [JsonPropertyName("processed_time"), JsonConverter(typeof(NullableDateTimeStringConverter))]
        public DateTime? ProcessedTime { get; set; }

        [JsonPropertyName("iso_order_time"), JsonConverter(typeof(NullableDateTimeStringConverter))]
        public DateTime? IsoOrderTime { get; set; }

        [JsonPropertyName("iso_processed_time"), JsonConverter(typeof(NullableDateTimeStringConverter))]
        public DateTime? IsoProcessedTime { get; set; }

        [JsonPropertyName("store_id"), JsonConverter(typeof(IntStringConverter))]
        public int StoreId { get; set; }

        [JsonPropertyName("ship_method_name")]
        public string ShipMethodName { get; set; }

        [JsonPropertyName("ship_method_id"), JsonConverter(typeof(IntStringConverter))]
        public int ShipMethodId { get; set; }

        [JsonPropertyName("status_id"), JsonConverter(typeof(OrderStatusStringConverter))]
        public OrderStatus Status { get; set; }

        [JsonPropertyName("weight"), JsonConverter(typeof(DecimalStringConverter))]
        public decimal Weight { get; set; }

        [JsonPropertyName("ship_total"), JsonConverter(typeof(DecimalStringConverter))]
        public decimal ShipTotal { get; set; }

        [JsonPropertyName("buyer_note")]
        public string BuyerNote { get; set; }

        [JsonPropertyName("total_quantity"), JsonConverter(typeof(IntStringConverter))]
        public int TotalQuantity { get; set; }

        [JsonPropertyName("total_lots"), JsonConverter(typeof(IntStringConverter))]
        public int TotalLots { get; set; }

        [JsonPropertyName("base_currency")]
        public string BaseCurrency { get; set; }

        [JsonPropertyName("payment_method_type")]
        public string PaymentMethodType { get; set; }

        [JsonPropertyName("payment_currency")]
        public string PaymentCurrency { get; set; }

        [JsonPropertyName("payment_total"), JsonConverter(typeof(DecimalStringConverter))]
        public decimal PaymentTotal { get; set; }

        [JsonPropertyName("base_order_total"), JsonConverter(typeof(DecimalStringConverter))]
        public decimal BaseOrderTotal { get; set; }

        [JsonPropertyName("sub_total"), JsonConverter(typeof(DecimalStringConverter))]
        public decimal SubTotal { get; set; }

        [JsonPropertyName("coupon_discount"), JsonConverter(typeof(DecimalStringConverter))]
        public decimal CouponDiscount { get; set; }

        [JsonPropertyName("payment_method_note")]
        public string PaymentMethodNote { get; set; }

        [JsonPropertyName("payment_transaction_id")]
        public string PaymentTransactionId { get; set; }

        [JsonPropertyName("tax_rate"), JsonConverter(typeof(IntStringConverter))]
        public int TaxRate { get; set; }

        [JsonPropertyName("tax_amount"), JsonConverter(typeof(DecimalStringConverter))]
        public decimal TaxAmount { get; set; }

        [JsonPropertyName("tax_scheme_id")]
        public string TaxSchemeId { get; set; }

        [JsonPropertyName("tracking_number")]
        public string TrackingNumber { get; set; }

        [JsonPropertyName("buyer_name")]
        public string BuyerName { get; set; }

        [JsonPropertyName("combine_with")]
        public string CombineWith { get; set; }

        [JsonPropertyName("refund_shipping"), JsonConverter(typeof(DecimalStringConverter))]
        public decimal RefundShipping { get; set; }

        [JsonPropertyName("refund_adjustment"), JsonConverter(typeof(DecimalStringConverter))]
        public decimal RefundAdjustment { get; set; }

        [JsonPropertyName("refund_subtotal"), JsonConverter(typeof(DecimalStringConverter))]
        public decimal RefundSubtotal { get; set; }

        [JsonPropertyName("refund_total"), JsonConverter(typeof(DecimalStringConverter))]
        public decimal RefundTotal { get; set; }
        
        [JsonPropertyName("refund_note")]
        public string RefundNote { get; set; }
        
        [JsonPropertyName("customer_feedback_left"), JsonConverter(typeof(NullableIntStringConverter))]
        public int? CustomerFeedbackLeft { get; set; }        

        [JsonPropertyName("store_feedback_left"), JsonConverter(typeof(NullableIntStringConverter))]
        public int? StoreFeedbackLeft { get; set; }

        [JsonPropertyName("my_cost_total"), JsonConverter(typeof(DecimalStringConverter))]
        public decimal MyCostTotal { get; set; }

        [JsonPropertyName("affiliate_fee"), JsonConverter(typeof(DecimalStringConverter))]
        public decimal AffiliateFee { get; set; }

        [JsonPropertyName("brickowl_fee"), JsonConverter(typeof(DecimalStringConverter))]
        public decimal BrickowlFee { get; set; }

        [JsonPropertyName("seller_note")]
        public string SellerNote { get; set; }

        [JsonPropertyName("customer_email")]
        public string CustomerEmail { get; set; }

        [JsonPropertyName("customer_user_id"), JsonConverter(typeof(IntStringConverter))]
        public int CustomerUserId { get; set; }

        [JsonPropertyName("customer_username")]
        public string CustomerUsername { get; set; }

        [JsonPropertyName("message_count"), JsonConverter(typeof(IntStringConverter))]
        public int MessageCount { get; set; }

        [JsonPropertyName("utm_source")]
        public string UtmSource { get; set; }

        [JsonPropertyName("utm_medium")]
        public string UtmMedium { get; set; }

        [JsonPropertyName("ship_first_name")]
        public string ShipFirstName { get; set; }

        [JsonPropertyName("ship_last_name")]
        public string ShipLastName { get; set; }

        [JsonPropertyName("ship_country_code")]
        public string ShipCountryCode { get; set; }

        [JsonPropertyName("ship_country")]
        public string ShipCountry { get; set; }

        [JsonPropertyName("ship_post_code")]
        public string ShipPostCode { get; set; }

        [JsonPropertyName("ship_street_1")]
        public string ShipStreet1 { get; set; }

        [JsonPropertyName("ship_street_2")]
        public string ShipStreet2 { get; set; }

        [JsonPropertyName("ship_city")]
        public string ShipCity { get; set; }

        [JsonPropertyName("ship_region")]
        public string ShipRegion { get; set; }

        [JsonPropertyName("ship_phone")]
        public string ShipPhone { get; set; }

        [JsonPropertyName("ship_tax")]
        public string ShipTax { get; set; }

        [JsonPropertyName("billing_first_name")]
        public string BillingFirstName { get; set; }

        [JsonPropertyName("billing_last_name")]
        public string BillingLastName { get; set; }

        [JsonPropertyName("billing_country_code")]
        public string BillingCountryCode { get; set; }

        [JsonPropertyName("billing_country")]
        public string BillingCountry { get; set; }

        [JsonPropertyName("billing_post_code")]
        public string BillingPostCode { get; set; }

        [JsonPropertyName("billing_street_1")]
        public string BillingStreet1 { get; set; }

        [JsonPropertyName("billing_street_2")]
        public string BillingStreet2 { get; set; }

        [JsonPropertyName("billing_city")]
        public string BillingCity { get; set; }

        [JsonPropertyName("billing_region")]
        public string BillingRegion { get; set; }

        [JsonPropertyName("billing_phone")]
        public string BillingPhone { get; set; }

        [JsonPropertyName("billing_tax")]
        public string BillingTax { get; set; }
    }
}
