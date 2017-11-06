using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using TheDream.Models;

namespace TheDream.Client
{
    public interface IApiClient
    {
        Task<HttpResponseMessage> GetFormEncodedContent(string requestUri, params KeyValuePair<string, string>[] values);
        Task<HttpResponseMessage> PostJsonEncodedContent<T>(string requestUri, T content) where T : ApiModel;
    }
}