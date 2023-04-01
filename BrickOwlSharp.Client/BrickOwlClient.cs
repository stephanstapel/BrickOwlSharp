using BrickOwlSharp.Client.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
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


        public async Task CreateInventoryAsync(
            NewInventory newInventory,
            CancellationToken cancellationToken = default)
        {
            Dictionary<string, string> formData = new Dictionary<string, string>();
            formData.Add("key", BrickOwlClientConfiguration.Instance.ApiKey);
            formData.Add("boid", newInventory.Id.ToString());
            formData.Add("quantity", newInventory.Quantity.ToString());
            formData.Add("price", newInventory.Price.ToString(CultureInfo.InvariantCulture));
            formData.Add("condition", EnumExtensions.ToDomainString(newInventory.Condition));

            var url = new Uri(_baseUri, $"inventory/create").ToString();
            NewInventoryResult result = await ExeucutePost<NewInventoryResult>(url, formData, cancellationToken: cancellationToken);
        }


        private static string AppendApiKey(string url)
        {
            BrickOwlClientConfiguration.Instance.ValidateThrowException();

            var uriBuilder = new UriBuilder(url);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["key"] = BrickOwlClientConfiguration.Instance.ApiKey;
            uriBuilder.Query = query.ToString();
            return uriBuilder.ToString();
        }


        private async Task<TResponse> ExecuteGet<TResponse>(string url, CancellationToken cancellationToken = default)
        {
            var urlWithKey = AppendApiKey(url);

            using (var message = new HttpRequestMessage(HttpMethod.Get, urlWithKey))
            {

                message.Content = null;
                var response = await _httpClient.SendAsync(message, cancellationToken);
                response.EnsureSuccessStatusCode();

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

                message.Content = null;
                var response = await _httpClient.PostAsync(url, content, cancellationToken);
                response.EnsureSuccessStatusCode();

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
    }
}