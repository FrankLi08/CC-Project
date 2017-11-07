using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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