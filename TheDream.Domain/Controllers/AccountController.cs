using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
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
        public async Task<ActionResult> Login(UserViewModel model)
        {
            var TokenResponse = await loginClient.Login(model);
            if (TokenResponse.StatusIsSuccessful)
            {
                FormAuthHelper.ValidateUser(model, TokenResponse, Response);
                
            }
            Session["token"] = TokenResponse.Data;
            return RedirectToAction("Index", "Home");
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
        public 
    }
}