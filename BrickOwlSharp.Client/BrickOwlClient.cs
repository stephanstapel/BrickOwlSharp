using BrickOwlSharp.Client.Extensions;
using BrickOwlSharp.Client.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Reflection;
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

        public BrickOwlClient(HttpClient httpClient,
            bool disposeHttpClient)
        {
            _httpClient = httpClient;
            _disposeHttpClient = disposeHttpClient;
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
            CancellationToken cancellationToken = default)
        {
            var url = new Uri(_baseUri, $"orders/list").ToString();
            return await ExecuteGet<List<Order>>(url, cancellationToken);
        }


        public async Task<List<Wishlist>> GetWishlistsAsync(
           CancellationToken cancellationToken = default)
        {
            var url = new Uri(_baseUri, $"wishlist/lists").ToString();
            return await ExecuteGet<List<Wishlist>>(url, cancellationToken);
        }


        public async Task<NewInventoryResult> CreateInventoryAsync(
            NewInventory newInventory,
            CancellationToken cancellationToken = default)
        {
            Dictionary<string, string> formData = _ObjectToFormData(newInventory);

            var url = new Uri(_baseUri, $"inventory/create").ToString();
            NewInventoryResult result = await ExeucutePost<NewInventoryResult>(url, formData, cancellationToken: cancellationToken);
            return result;
        }        


        public async Task<bool> UpdateInventoryAsync(
            UpdateInventory updatedInventory,
            CancellationToken cancellationToken = default)
        {
            Dictionary<string, string> formData = _ObjectToFormData(updatedInventory);

            var url = new Uri(_baseUri, $"inventory/update").ToString();
            BrickOwlResult result = await ExeucutePost<BrickOwlResult>(url, formData, cancellationToken: cancellationToken);
            return (result?.Status == "success");
        }


        public async Task<List<Inventory>> GetInventoryAsync(
            string filter = null, bool? activeOnly = null, string externalId = null, int? lotId = null,
            CancellationToken cancellationToken = default)
        {
            var url = new Uri(_baseUri, $"inventory/list").ToString();

            url = AppendOptionalParam(url, "filter", filter);
            url = AppendOptionalParam(url, "active_only", activeOnly);
            url = AppendOptionalParam(url, "external_id_1", externalId);
            url = AppendOptionalParam(url, "lot_id", lotId);

            return await ExecuteGet<List<Inventory>>(url, cancellationToken);
        } // !GetInventoryAsync()


        public async Task<bool> DeleteInventoryAsync(
           DeleteInventory deleteInventory,
           CancellationToken cancellationToken = default)
        {
            Dictionary<string, string> formData = _ObjectToFormData(deleteInventory);

            var url = new Uri(_baseUri, $"inventory/delete").ToString();
            BrickOwlResult result = await ExeucutePost<BrickOwlResult>(url, formData, cancellationToken: cancellationToken);
            return (result?.Status == "success");
        } // !GetInventoryAsync()


        private static string AppendApiKey(string url)
        {
            BrickOwlClientConfiguration.Instance.ValidateThrowException();
            return AppendOptionalParam(url, "key", BrickOwlClientConfiguration.Instance.ApiKey);
        } // !AppendApiKey()


        private static string AppendOptionalParam(string url, string key, object value)
        {
            if (value == null)
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
        } // !ExecuteGet
          

        private async Task<TResponse> ExeucutePost<TResponse>(string url, Dictionary<string, string> formData, CancellationToken cancellationToken = default)
        {            
            using (var message = new HttpRequestMessage(HttpMethod.Post, url))
            {
                HttpContent content = new FormUrlEncodedContent(formData);
                var response = await _httpClient.PostAsync(url, content, cancellationToken);
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
        } // !ExeucutePost()


        Dictionary<string, string> _ObjectToFormData(object o, bool addKey = true)
        {
            Dictionary<string, string> retval = new Dictionary<string, string>();

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
                    retval.Add(name, value);
                }
            }

            if (addKey)
            {
                retval.Add("key", BrickOwlClientConfiguration.Instance.ApiKey);
            }

            return retval;
        } // !_ObjectToFormData()
    }
}