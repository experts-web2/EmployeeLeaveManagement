using Blazored.LocalStorage;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace ELM.Shared
{
    public class HttpClientService : IHttpClientService
    {
       
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;
        private readonly ILocalStorageService _localStorage;

        private string AccessToken { get; set; }

        public HttpClientService(ILocalStorageService localStorage, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
            _localStorage = localStorage;
        }

        #region public methods

        public async Task<HttpResponseMessage> PostAsync<T>(T model, string path) where T : class
        {
            try
            {
                HttpContent stringContent = SetHttpContent(model);
                var httpClient = await GetHttpClient();
                var result = await httpClient.PostAsync(path, stringContent);
                if (!result.IsSuccessStatusCode)
                    throw new InvalidOperationException(result.ReasonPhrase);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<returnT> PostAsJsonAsync<returnT, modelT>(modelT model, string path) where modelT : class
        {
            try
            {
                var httpClient = await GetHttpClient();
                var result = await httpClient.PostAsJsonAsync(path, model);

                if (!result.IsSuccessStatusCode)
                    throw new InvalidOperationException(result.ReasonPhrase);
                var jsonString = await result.Content.ReadAsStringAsync();

                var returnObject = JsonConvert.DeserializeObject<returnT>(jsonString);
                return returnObject;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<HttpResponseMessage> Post<T>(T model, string path) where T : class
        {
            try
            {
                HttpContent stringContent = SetHttpContent(model);
                var httpClient = await GetHttpClient();
                var result = await httpClient.PostAsync(path, stringContent);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T> PutAsync<T>(T model, string path) where T : class
        {
            try
            {
                HttpContent stringContent = SetHttpContent(model);
                var httpClient = await GetHttpClient();
                var result = await httpClient.PutAsync(path, stringContent);
                return await DeserializeAsync<T>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T> PutAsync<T>(string path)
        {
            try
            {
                var httpClient = await GetHttpClient();
                var response = await httpClient.GetAsync(path);

                if (!response.IsSuccessStatusCode)
                    throw new Exception(response.ReasonPhrase);

                return await DeserializeAsync<T>(response);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<HttpResponseMessage> DeleteAsync(string path)
        {
            try
            {
                var httpClient = await GetHttpClient();
                return await httpClient.DeleteAsync(path);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T> GetAsync<T>(string path)
        {
            try
            {
                var httpClient = await GetHttpClient();
                var response = await httpClient.GetAsync(path);

                if (!response.IsSuccessStatusCode)
                    throw new Exception(response.ReasonPhrase);

                return await DeserializeAsync<T>(response);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<T> SendAsync<T, TModel>(string path, TModel model, HttpMethod method)
        {

            try
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = method,
                    RequestUri = new Uri(configuration["BaseApiLocation"] + path),
                    Content = SetHttpContent(model)
                };
                var httpClient = await GetHttpClient(false);
                var response = await httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                    throw new Exception(response.ReasonPhrase);

                return await DeserializeAsync<T>(response);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T> DeserializeAsync<T>(HttpResponseMessage response)
        {
            string serializedString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(serializedString);
        }

        public string Serialize<T>(T model)
        {
            return JsonConvert.SerializeObject(model);
        }

        #endregion

        #region private methods

        /// <summary>
        /// Gets http client to make http requests.
        /// </summary>
        /// <returns><seealso cref="HttpClient"/></returns>
        private async Task<HttpClient> GetHttpClient(bool namedClient = true)
        {
            try
            {
                var savedToken = await _localStorage.GetItemAsync<string>("authToken");

                HttpClient httpClient = namedClient ? httpClientFactory.CreateClient("RetailApi") : httpClientFactory.CreateClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);

                return httpClient;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Sets <seealso cref="HttpContent"/> for http request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns><seealso cref="HttpContent"/></returns>
        private HttpContent SetHttpContent<T>(T model)
        {
            string serializedString = Serialize(model);
            HttpContent stringContent = new StringContent(serializedString, Encoding.UTF8, MediaTypeNames.Application.Json);
            return stringContent;
        }

        #endregion
    }
}
    
