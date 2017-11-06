using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TheDream.Client;
using TheDream.Extension;
using TheDream.Models;

namespace TheDream.Controllers
{
    [RoutePrefix("Account")]
    public class AccountController : Controller
    {
        private readonly LoginClient loginClient;
        // GET: Account
        public AccountController()
        {

            var apiClient = new ApiClient(HttpClientInstance.Instance);
            loginClient = new LoginClient(apiClient);
        }
        
        public ActionResult Index()
        {
            return View();
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
            var response = await loginClient.Login(model);
            Session["token"] = response.Data;
            return RedirectToAction("Index", "Home");
        }
    }
}