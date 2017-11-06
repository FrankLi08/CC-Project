using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TheDream.Domain.Models;

namespace TheDream.Domain.Client
{
  public interface IApiClient
    {
        Task<HttpResponseMessage> GetFormEncodedContent(string requestUri, params KeyValuePair<string, string>[] values);
        Task<HttpResponseMessage> PostJsonEncodedContent<T>(string requestUri, T content) where T : ApiModel;
    }

}
