using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.DataHandler.Serializer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;

using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using TheDream.Domain.Client;
using TheDream.Domain.Extension;
using TheDream.Domain.Helper;
using TheDream.Domain.Models;
using TheDream.Domain.Repository;
using TheDream.Model.Models;

namespace TheDream.Domain.Controllers
{
    [RoutePrefix("Account")]

    public class AccountController : Controller
    {
        private readonly LoginClient loginClient;
        private readonly HttpClientRepo _ClientRepo;
        private string xxxx;
        // GET: Account
        public AccountController(HttpClientRepo ClientRepo)
        {

            var apiClient = new ApiClient(HttpClientInstance.Instance);
            loginClient = new LoginClient(apiClient);
            _ClientRepo = ClientRepo;
          
        }
        
     
        [Route("LogIn")]
        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }


        [HttpPost]
        public ActionResult Login(UserViewModel model)
        { 
            string TokenUri = "Http://localhost:62367/api/Token";
        //var getTokenUrl = string.Format(ApiEndPoints.AuthorisationTokenEndpoint.Post.Token, ConfigurationManager.AppSettings["ApiBaseUri"]);

            using (HttpClient httpClient = new HttpClient())
            {
                HttpContent content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", model.UserName),
                    new KeyValuePair<string, string>("password", model.Password)
                });

                HttpResponseMessage Responses =  httpClient.PostAsync(TokenUri, content).Result;
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                string resultContent = Responses.Content.ReadAsStringAsync().Result;

                var result = JsonConvert.DeserializeObject<Token>(resultContent);
                //
                FormsAuthentication.SetAuthCookie(result.AccessToken, true);
                AuthenticationProperties options = new AuthenticationProperties();

                options.AllowRefresh = true;
                options.IsPersistent = true;
                //options.ExpiresUtc = DateTime.UtcNow.AddSeconds(int.Parse(token.expires_in));

                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, result.UserName), //Name is the default name claim type, and UserName is the one known also in Web API.
                    new Claim(ClaimTypes.NameIdentifier, result.UserName), //If you want to use User.Identity.GetUserId in Web API, you need a NameIdentifier 
                    new Claim("AcessToken", result.AccessToken)
                };

                var identity = new ClaimsIdentity(claims, "ApplicationCookie");
                var authTicket = new AuthenticationTicket(new ClaimsIdentity(claims, "ApplicationCookie"), new AuthenticationProperties
                {
                    ExpiresUtc = result.Expires,
                    
                    IssuedUtc = result.Issued
                   
                });
                byte[] userData = DataSerializers.Ticket.Serialize(authTicket);
                string protectedText = TextEncodings.Base64Url.Encode(userData);
                Response.SetCookie(new HttpCookie("CloudChef.WebApi.Auth")
                {
                    HttpOnly = true,
                    Expires = result.Expires.UtcDateTime,
                    Value = protectedText
                });
                var claimidentity = (ClaimsIdentity)User.Identity;
                IEnumerable<Claim> claimsI = claimidentity.Claims;
                string BToken =claimsI.Where(x => x.Type == "AcessToken").Select(x => x.Value).FirstOrDefault().ToString();
                Configuration webConfigApp = WebConfigurationManager.OpenWebConfiguration("~");
                var settings = webConfigApp.AppSettings.Settings;
                if (settings["BearerToken"] == null)
                {
                    settings.Add("BearerToken", BToken);
                }
                else
                {
                    settings["BearerToken"].Value = BToken;
                }
                webConfigApp.Save(ConfigurationSaveMode.Modified);
           
                Request.GetOwinContext().Authentication.SignIn(options, identity);

            }
           

            return RedirectToAction("Index", "Home");
            //var TokenResponse = await loginClient.Login(model);
            //if (TokenResponse.StatusIsSuccessful)
            //{
            //    FormAuthHelper.ValidateUser(model, TokenResponse, Response);

            //}
            //Session["token"] = TokenResponse.Data;
            //return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SignUp(Users model)
        {
            if (ModelState.IsValid)
            {
                var Response = await _ClientRepo.SignUp(model);
                if (Response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Login");
                }
                else if (Response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    ViewBag.error = "Please Try again";
                    return View();
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> ChangePassword(ChangePasswordModel model)
        {
            model.username = User.Identity.GetUserName();
            if (ModelState.IsValid)
            {
                if (model.password == model.newPassword)
                {
                    ViewBag.Error = "The new Password can not be same as old Password";
                    return View(model);
                }
                var response = await _ClientRepo.ChangePassword(model);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("LogIn");
                }
                else
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.BadRequest:
                            ViewBag.Error = "please enter the password and new password again";
                            break;
                        case HttpStatusCode.NotFound:
                            ViewBag.Error = "User is not found";
                            break;
                        case HttpStatusCode.InternalServerError:
                            ViewBag.Error = "Please try again";
                            break;
                    }
                    return View(model);
                }
            }
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> ResetPassword(string userName)
        {
            if (userName == "" || userName == null)
            {
                ViewBag.error = "Please enter your user name";
                return View();
            }
            //Post data to API to Get new password
            var Response = await _ClientRepo.ResetPassword(userName);
            if (Response.IsSuccessStatusCode || Response.StatusCode == HttpStatusCode.NotFound)
            {
                // If success, display "An email has been sent"
                ViewBag.success = "Please check your new password in the E-Mail";
            }
            else if (Response.StatusCode == HttpStatusCode.NotFound)
            {
                // If not, display error message
                ViewBag.error = "Operation failed, unable to reset password";
            }
            else
            {
                ViewBag.error = "Unknown error, unable to reset password";
            }
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> UpdateUser()
        {
            string userName = User.Identity.GetUserName();
            var response = await _ClientRepo.GetUserInfo(userName);
            UpdateUser updateUser = null;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                updateUser = JsonConvert.DeserializeObject<UpdateUser>(result);

            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                ViewBag.Error = "User not exist";
                return RedirectToAction("HomeIndex");
                //need to change later
            }
            return View(updateUser);
        }
        [HttpPost]
        public async Task<ActionResult> UpdateUser(UpdateUser model)
        {
            model.username = User.Identity.GetUserName();
            var response = await _ClientRepo.UpdateUser(model);
            return View();
            //need to change


        }
    }
}