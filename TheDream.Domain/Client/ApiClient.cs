using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using TheDream.Domain.Models;

namespace TheDream.Domain.Client
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient httpClient;
        public ApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<HttpResponseMessage> GetFormEncodedContent(string requestUri, params KeyValuePair<string, string>[] values)
        {
            using (var content = new FormUrlEncodedContent(values))
            {
                var query = await content.ReadAsStringAsync();
                var requestUriWithQuery = string.Concat(requestUri, "?", query);
                var response = await httpClient.GetAsync(requestUriWithQuery);
                return response;
            }
        }

        public async Task<HttpResponseMessage> PostJsonEncodedContent<T>(string requestUri, T content) where T : ApiModel
        {
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var response = await httpClient.PostAsJsonAsync(requestUri, content);
            return response;
        }
        public async Task<HttpResponseMessage> PostFormEncodedContent(string requestUri, params KeyValuePair<string, string>[] values)
        {
            using (var content = new FormUrlEncodedContent(values))
            {
                var response = await httpClient.PostAsync(requestUri, content);
                return response;
            }
        }
    }
}