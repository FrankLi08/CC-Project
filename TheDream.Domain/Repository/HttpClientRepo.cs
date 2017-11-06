using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using TheDream.Domain.Repository.Interface;
using TheDream.Model.Models;

namespace TheDream.Domain.Repository
{
    public class HttpClientRepo : IHttpClientRepo
    {
        private readonly IUserSession _userSession;
        public HttpClientRepo(IUserSession userSession)
        {
            _userSession = userSession;
        }
        string ApiBaseUrl = "Http://localhost:62367/";
        public async Task<HttpResponseMessage> GetRoles(string userName)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _userSession.BearerToken);
                var AllRoleUrl = ApiBaseUrl + "/api/Account/GetAllRoles";
                var responseMessage = await client.PostAsJsonAsync(AllRoleUrl,userName);
                return responseMessage;
            }
        }

        public  async Task<HttpResponseMessage> SignUp(Users model)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _userSession.BearerToken);
                var AllRoleUrl = ApiBaseUrl + "/api/Account/SignUp";
                var responseMessage = await client.PostAsJsonAsync(AllRoleUrl, model);
                return responseMessage;
            }
        }
    }
}