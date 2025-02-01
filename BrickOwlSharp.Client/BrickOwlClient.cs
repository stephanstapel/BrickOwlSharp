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
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace BrickOwlSharp.Client
{
    internal sealed class BrickOwlClient : IBrickOwlClient
    {
        private static readonly Uri _baseUri = new Uri("https://api.brickowl.com/v1/");
        private readonly HttpClient _httpClient;
        private readonly bool _disposeHttpClient;

        private bool _isDisposed;
        private IBrickOwlRequestHandler _requestHandler;

        public BrickOwlClient(HttpClient httpClient,
            bool disposeHttpClient,
            IBrickOwlRequestHandler requestHandler = null)
        {
            _httpClient = httpClient;
            _disposeHttpClient = disposeHttpClient;
            _requestHandler = requestHandler;
        }

        ~BrickOwlClient()
        {
            Dispose(false);
        }

        private static JsonSerializerOptions IgnoreNullValuesJsonSerializerOptions
        {
            get
            {
                return new JsonSerializerOptions()
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };
            }
        }

        private void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            {
                if (_disposeHttpClient)
                {
                    _httpClient.Dispose();
                }
            }

            _isDisposed = true;
        }


        public async Task<List<Order>> GetOrdersAsync(
            OrderStatus? orderStatusFilter = null,
            DateTime? minOrderTime = null,
            int? limit = null,
            OrderType? orderType = null,
            OrderSortType? orderSortType = null,
            CancellationToken cancellationToken = default)
        {
            var url = new Uri(_baseUri, $"order/list").ToString();

            if (orderStatusFilter.HasValue)
            {
                url = AppendOptionalParam(url, "status", (int)orderStatusFilter);
            }

            if (minOrderTime.HasValue)
            {
                url = AppendOptionalParam(url, "order_time", ((DateTimeOffset)minOrderTime.Value).ToUnixTimeSeconds());
            }

            if (limit.HasValue)
            {
                url = AppendOptionalParam(url, "limit", limit);
            }

            if (orderType.HasValue)
            {
                if (orderType == OrderType.Placed)
                {
                    url = AppendOptionalParam(url, "list_type", "customer");
                }
                else if (orderType == OrderType.Received)
                {
                    url = AppendOptionalParam(url, "list_type", "store");
                }
            }

            if (orderSortType.HasValue)
            {
                url = AppendOptionalParam(url, "sort_by", orderSortType.Value.ToString().ToLower());
            }

            List<Order> result = await ExecuteGet<List<Order>>(url, cancellationToken);
            _measureRequest(ResourceType.Order, cancellationToken);
            return result;
        } // !GetOrdersAsync()


        public async Task<OrderDetails> GetOrderAsync(int orderId,  CancellationToken cancellationToken = default)
        {
            var detailsUrl = new Uri(_baseUri, $"order/view").ToString();
            detailsUrl = AppendOptionalParam(detailsUrl, "order_id", orderId);
            OrderDetails details = await ExecuteGet<OrderDetails>(detailsUrl, cancellationToken);
            _measureRequest(ResourceType.Order, cancellationToken);

            var itemUrl = new Uri(_baseUri, $"order/items").ToString();
            itemUrl = AppendOptionalParam(itemUrl, "order_id", orderId);
            List<OrderItem> items = await ExecuteGet<List<OrderItem>>(itemUrl, cancellationToken);
            _measureRequest(ResourceType.Order, cancellationToken);
            details.OrderItems = items;
            return details;
        } // !GetOrderAsync()


        public async Task<bool> UpdateOrderStatusAsync(int orderId, OrderStatus status, CancellationToken cancellationToken = default)
        {
            Dictionary<string, string> formData = new Dictionary<string, string>()
            {
                { "order_id", orderId.ToString() },
                { "status_id", ((int)status).ToString() },
                { "key", BrickOwlClientConfiguration.Instance.ApiKey }
            };

            var url = new Uri(_baseUri, $"order/set_status").ToString();

            try
            {
                BrickOwlResult result = await ExecutePost<BrickOwlResult>(url, formData, cancellationToken: cancellationToken);
                _measureRequest(ResourceType.Order, cancellationToken);
                if (string.Equals(result.Status, "success", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        } // !UpdateOrderStatusAsync()

        public async Task<bool> UpdateOrderTrackingAsync(int orderId, string trackingIdOrUrl, CancellationToken cancellationToken = default)
        {
        	Dictionary<string, string> formData = new Dictionary<string, string>
        	{
        		{ "order_id", orderId.ToString() },
        		{ "tracking_id", trackingIdOrUrl },
        		{ "key", BrickOwlClientConfiguration.Instance.ApiKey }
        	};
        
        	var url = new Uri(_baseUri, "order/tracking").ToString();
        
        	try
        	{
        		BrickOwlResult result = await ExecutePost<BrickOwlResult>(url, formData, cancellationToken: cancellationToken);
        		_measureRequest(ResourceType.Order, cancellationToken);
        		return string.Equals(result.Status, "success", StringComparison.OrdinalIgnoreCase);
        	}
        	catch
        	{
        		return false;
        	}
        } // !UpdateOrderTrackingAsync()

        public async Task<List<Wishlist>> GetWishlistsAsync(
           CancellationToken cancellationToken = default)
        {
            var url = new Uri(_baseUri, $"wishlist/lists").ToString();
            List<Wishlist> result = await ExecuteGet<List<Wishlist>>(url, cancellationToken);
            _measureRequest(ResourceType.Wishlist, cancellationToken);
            return result;
        }


        public async Task<List<CatalogItem>> GetCatalogAsync(
           CancellationToken cancellationToken = default)
        {
            var url = new Uri(_baseUri, $"catalog/list").ToString();
            List<CatalogItem> result = await ExecuteGet<List<CatalogItem>>(url, cancellationToken);
            _measureRequest(ResourceType.Catalog, cancellationToken);
            return result;
        }


        public async Task<Dictionary<string,CatalogItemAvailability>> CatalogAvailabilityAsync(string boid, string country, int? quantity = null, CancellationToken cancellationToken = default)
        {
            var url = new Uri(_baseUri, $"catalog/availability").ToString();
            url = AppendOptionalParam(url, "boid", boid);
            url = AppendOptionalParam(url, "country", country);

            if (quantity.HasValue)
            {
                url = AppendOptionalParam(url, "quantity", quantity.ToString());
            }

            Dictionary<string, CatalogItemAvailability> result = await ExecuteGet<Dictionary<string, CatalogItemAvailability>>(url, cancellationToken);
            _measureRequest(ResourceType.Catalog, cancellationToken);
            return result;
        }


        public async Task<CatalogItem> CatalogLookupAsync(string boid, CancellationToken cancellationToken = default)
        {
            var url = new Uri(_baseUri, $"catalog/lookup").ToString();
            url = AppendOptionalParam(url, "boid", boid);
            CatalogItem result = await ExecuteGet<CatalogItem> (url, cancellationToken);
            _measureRequest(ResourceType.Catalog, cancellationToken);
            return result;
        }


        public async Task<List<string>> CatalogIdLookupAsync(string boid, ItemType type, IdType? idType = null, CancellationToken cancellationToken = default)
        {
            var url = new Uri(_baseUri, $"catalog/id_lookup").ToString();
            url = AppendOptionalParam(url, "id", boid);
            url = AppendOptionalParam(url, "type", type.ToString());

            if (idType.HasValue)
            {
                url = AppendOptionalParam(url, "id_type", idType.Value.EnumToString());
            }

            CatalogItemIds result = await ExecuteGet<CatalogItemIds>(url, cancellationToken);
            _measureRequest(ResourceType.Catalog, cancellationToken);
            return result.BOIDs;
        } // !CatalogIdLookupAsync()


        public async Task<NewInventoryResult> CreateInventoryAsync(
            NewInventory newInventory,
            CancellationToken cancellationToken = default)
        {
            Dictionary<string, string> formData = _ObjectToFormData(newInventory);

            if (!newInventory.ColorId.HasValue)
            {
                formData.Remove("color_id");
            }

            var url = new Uri(_baseUri, $"inventory/create").ToString();
            NewInventoryResult result = await ExecutePost<NewInventoryResult>(url, formData, cancellationToken: cancellationToken);
            _measureRequest(ResourceType.Inventory, cancellationToken);
            return result;
        } // !CreateInventoryAsync()    


        public async Task<bool> UpdateInventoryAsync(
            UpdateInventory updatedInventory,
            CancellationToken cancellationToken = default)
        {
            Dictionary<string, string> formData = _ObjectToFormData(updatedInventory);

            var url = new Uri(_baseUri, $"inventory/update").ToString();
            BrickOwlResult result = await ExecutePost<BrickOwlResult>(url, formData, cancellationToken: cancellationToken);
            _measureRequest(ResourceType.Inventory, cancellationToken);
            return (result?.Status == "success");
        } // !UpdateInventoryAsync()


        public async Task<List<Inventory>> GetInventoryAsync(
            string filter = null, bool? activeOnly = null, string externalId = null, int? lotId = null,
            CancellationToken cancellationToken = default)
        {
            var url = new Uri(_baseUri, $"inventory/list").ToString();

            url = AppendOptionalParam(url, "filter", filter);

            if (activeOnly.HasValue)
            {               
                url = AppendOptionalParam(url, "active_only", activeOnly.Value == true ? 1 : 0);
            }

            url = AppendOptionalParam(url, "external_id_1", externalId);
            url = AppendOptionalParam(url, "lot_id", lotId);

            List<Inventory> result = await ExecuteGet<List<Inventory>>(url, cancellationToken);
            _measureRequest(ResourceType.Inventory, cancellationToken);
            return result;
        } // !GetInventoryAsync()


        public async Task<bool> DeleteInventoryAsync(
           DeleteInventory deleteInventory,
           CancellationToken cancellationToken = default)
        {
            Dictionary<string, string> formData = _ObjectToFormData(deleteInventory);            

            var url = new Uri(_baseUri, $"inventory/delete").ToString();
            BrickOwlResult result = await ExecutePost<BrickOwlResult>(url, formData, cancellationToken: cancellationToken);
            _measureRequest(ResourceType.Inventory, cancellationToken);
            return (result?.Status == "success");
        } // !GetInventoryAsync()


        public async Task<List<Color>> GetColorListAsyn(CancellationToken cancellationToken = default)
        {
            var url = new Uri(_baseUri, $"catalog/color_list").ToString();

            Dictionary<string, Color> result = await ExecuteGet<Dictionary<string, Color>>(url, cancellationToken);
            _measureRequest(ResourceType.Catalog, cancellationToken);
            return result.Values.ToList();
        } // !GetColorListAsyn()



        private static string AppendApiKey(string url)
        {
            BrickOwlClientConfiguration.Instance.ValidateThrowException();
            return AppendOptionalParam(url, "key", BrickOwlClientConfiguration.Instance.ApiKey);
        } // !AppendApiKey()


        private static string AppendOptionalParam(string url, string key, object value)
        {
            if (value is null)
            {
                return url;
            }

            var uriBuilder = new UriBuilder(url);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query[key] = value.ToString();
            uriBuilder.Query = query.ToString();
            return uriBuilder.ToString();
        } // !AppendOptionalParam()


        private async Task<TResponse> ExecuteGet<TResponse>(string url, CancellationToken cancellationToken = default)
        {
            var urlWithKey = AppendApiKey(url);

            using (var message = new HttpRequestMessage(HttpMethod.Get, urlWithKey))
            {

                message.Content = null;
                var response = await _httpClient.SendAsync(message, cancellationToken);
                
                try
                {
                    response.EnsureSuccessStatusCode();
                }
                catch
                {
                    throw new HttpRequestException($"Received status code {response.StatusCode} for url {url}");
                }

                var contentAsString = await response.Content.ReadAsStringAsync();
                var responseData = JsonSerializer.Deserialize<TResponse>(contentAsString);

                if (responseData == null)
                {
                    //TODO 
                    throw new Exception("");
                }

                return responseData;
            }            
        } // !ExecuteGet()
          

        private async Task<TResponse> ExecutePost<TResponse>(string url, Dictionary<string, string> formData, CancellationToken cancellationToken = default)
        {            
            using (var message = new HttpRequestMessage(HttpMethod.Post, url))
            {
                HttpContent content = new FormUrlEncodedContent(formData);
                HttpResponseMessage response = null;
                try
                {
                    Task< HttpResponseMessage> responseTask = _httpClient.PostAsync(url, content, cancellationToken);
                    responseTask.Wait();
                    response = responseTask.Result;
                }
                catch (Exception ex)
                {
                    throw new HttpRequestException($"Could not execute request for url {url}");
                }

                try
                { 
                    response.EnsureSuccessStatusCode();
                }
                catch
                {
                    throw new HttpRequestException($"Received status code {response.StatusCode} for url {url} with form data {JsonSerializer.Serialize(formData)}");
                }

                var contentAsString = await response.Content.ReadAsStringAsync();
                var responseData = JsonSerializer.Deserialize<TResponse>(contentAsString);

                if (responseData == null)
                {
                    //TODO 
                    throw new Exception("");
                }

                return responseData;
            }
        } // !ExecutePost()


        private Dictionary<string, string> _ObjectToFormData(object o, bool addKey = true)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            PropertyInfo[] props = o.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                object[] attrs = prop.GetCustomAttributes(true);

                string name = null;
                object rawValue = null;
                string converterName = null;
                string value = null;

                foreach (object attr in attrs)
                {
                    if (attr.GetType() == typeof(JsonPropertyNameAttribute))
                    {
                        name = ((JsonPropertyNameAttribute)attr).Name;
                        rawValue = o.GetType().GetProperty(prop.Name).GetValue(o);
                    }
                    else if (attr.GetType() == typeof(JsonConverterAttribute))
                    {
                        converterName = ((JsonConverterAttribute)attr).ConverterType.Name;
                    }
                }

                if ((converterName == "ConditionStringConverter") && (rawValue != null))
                {
                    ConditionStringConverter csv = new ConditionStringConverter();
                    value = csv.Write((Condition)rawValue);
                }
                else if (String.IsNullOrEmpty(converterName) && (rawValue != null))
                {
                    if (rawValue is decimal)
                    {
                        value = ((decimal)rawValue).ToString(CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        value = rawValue.ToString();
                    }
                }

                if (value != null)
                {
                    result.Add(name, value);
                }
            }

            if (addKey)
            {
                result.Add("key", BrickOwlClientConfiguration.Instance.ApiKey);
            }

            return result;
        } // !_ObjectToFormData()


        private async void _measureRequest(ResourceType resourceType, CancellationToken cancellationToken = default)
        {
            if (this._requestHandler != null)
            {
                CancellationTokenSource source = new CancellationTokenSource();
                await this._requestHandler.OnRequestAsync(resourceType, cancellationToken);
            }
        }
    }
}
