using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using TheDream.Domain.Client;
using TheDream.Domain.Extension;
using TheDream.Domain.Response;

namespace TheDream.Domain.Models
{
    public interface ILoginClient
    {
        Task<TokenResponse> Login(UserViewModel model);
    }
    public class LoginClient : ClientBase
    {
        private const string TokenUri = "Http://localhost:62367/api/Token";

        public LoginClient(IApiClient apiClient) : base(apiClient)
        {
        }

        public async Task<TokenResponse> Login(UserViewModel model)
        {
            List<string> roles = new List<string>();
            using (var client = new HttpClient())
            {
                var values = new List<KeyValuePair<string, string>>();
                values.Add("grant_type".AsPair("password"));
                values.Add("username".AsPair(model.UserName));
                values.Add("password".AsPair(model.Password));
                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync(TokenUri, content);

                var tokenResponse = await CreateJsonResponse<TokenResponse>(response);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await DecodeContent<dynamic>(response);
                    tokenResponse.ErrorState = new ErrorStateResponse()
                    {
                        ModelState = new Dictionary<string, string[]>
                {
                    {errorContent["error"], new string[] {errorContent["error_description"]}}
                }
                    };
                    return tokenResponse;
                }
                var tokenData = await DecodeContent<dynamic>(response);
                tokenResponse.Data = tokenData["access_token"];
                tokenResponse.Role = tokenData["roles"];
             
                 AuthenticationProperties options = new AuthenticationProperties();

                options.AllowRefresh = true;
                options.IsPersistent = true;
                //options.ExpiresUtc = DateTime.UtcNow.AddSeconds(int.Parse(token.expires_in));

                var claims = new[]
                {
                    new Claim("AcessToken",tokenResponse.Data.ToString()),
                };

                var identity = new ClaimsIdentity(claims, "ApplicationCookie");
                HttpContext.Current.GetOwinContext().Authentication.SignIn(options, identity);
                return tokenResponse;
            }
        }

        public void LogOut()
        {
            HttpContext.Current.GetOwinContext().Authentication.SignOut("ApplicationCookie");
        }
    }
}