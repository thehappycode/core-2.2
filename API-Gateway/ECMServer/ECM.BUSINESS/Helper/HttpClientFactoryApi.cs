using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ECM.BUSINESS.Helper
{
    public static class HttpClientFactoryApi
    {
        public static async Task<T> Get<T>(HttpClient _client, string _requestUrl, string _accessToken) where T : new()
        {
            T results = new T();
            var accessTokens = _accessToken.Split(" ");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(accessTokens[0], accessTokens[1]);
            var request = new HttpRequestMessage(HttpMethod.Get, _requestUrl);
            var response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                results = JsonConvert.DeserializeObject<T>(responseContent.ToString());
            }
            return results;
        }
    }
}
