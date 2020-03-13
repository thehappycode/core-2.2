using BOS.BUSINESS.Infrastructures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BOS.BUSINESS.Services
{
    public static class HttpClientFactoryApi
    {
        public static async Task<T> GetAsync<T>(HttpClient _client, string _requestUrl, string _accessToken) where T: new()
        {
            T results = new T();
            var accessTokens = _accessToken.Split(" ");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(accessTokens[0], accessTokens[1]);
            HttpResponseMessage response = await _client.GetAsync(_requestUrl);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                results =  JsonConvert.DeserializeObject<T>(responseContent.ToString());
            }
            return results;
        }

        public static async Task<dynamic> PostAsync<T>(HttpClient _client, string _requestUrl, string _accessToken, T data)
        {
            dynamic results = null;
            var accessTokens = _accessToken.Split(" ");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(accessTokens[0], accessTokens[1]);
            var _content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(_requestUrl, _content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                results = responseContent;
            }
            return results;
        }
        public static async Task<dynamic> PutAsync<T>(HttpClient _client, string _requestUrl, string _accessToken, T data)
        {
            dynamic results = null;
            var accessTokens = _accessToken.Split(" ");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(accessTokens[0], accessTokens[1]);
            var _content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PutAsync(_requestUrl, _content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                results = responseContent;
            }
            return results;
        }
        public static async Task<dynamic> DeleteAsync(HttpClient _client, string _requestUrl, string _accessToken)
        {
            dynamic results = null;
            var accessTokens = _accessToken.Split(" ");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(accessTokens[0], accessTokens[1]);
            HttpResponseMessage response = await _client.DeleteAsync(_requestUrl);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                results = responseContent;
            }
            return results;
        }
    }
}
