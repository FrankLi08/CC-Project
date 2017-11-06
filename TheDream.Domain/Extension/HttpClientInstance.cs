using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace TheDream.Domain.Extension
{
    public static class HttpClientInstance
    {
        private const string BaseUri = "http://localhost:62367/";
        private static readonly HttpClient instance = new HttpClient { BaseAddress = new Uri(BaseUri) };

        public static HttpClient Instance
        {
            get { return instance; }
        }
    }
}