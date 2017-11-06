using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using TheDream.Repository.Interface;

namespace TheDream.Repository
{
    public class HttpClientRepo : IHttpClientRepo
    {
        private readonly IUserSession _userSession;
        public HttpClientRepo(IUserSession userSeesion)
        {
            _userSession = userSeesion;
        }
        string ApiBaseUrl = "Http://localhost:62367/";
        public async Task<HttpResponseMessage> GetRoles()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _userSession.BearerToken);
                var AllRoleUrl = ApiBaseUrl + "/api/Account/GetAllRoles";
                var responseMessage = await client.GetAsync(AllRoleUrl);
                return responseMessage;
            }
        }
    }
}