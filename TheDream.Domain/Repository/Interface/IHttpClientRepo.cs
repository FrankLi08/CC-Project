using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TheDream.Domain.Repository.Interface
{
   public  interface IHttpClientRepo
    {
        Task<HttpResponseMessage> GetRoles(string userName);
    }
}
