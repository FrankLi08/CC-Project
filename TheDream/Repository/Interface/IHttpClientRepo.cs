using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace TheDream.Repository.Interface
{
  public interface IHttpClientRepo
    {
        Task<HttpResponseMessage> GetRoles();
    }
}