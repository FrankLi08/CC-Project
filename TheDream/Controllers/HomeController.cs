using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace TheDream.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            if (Session["token"] == null)
            {
                return View();
            }
            else
            {
                var bearerToken = Session["token"].ToString();

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:62367/api/Story/GetStory");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
                HttpResponseMessage responseMessage = await client.GetAsync("http://localhost:62367/api/Story/GetStory");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    TempData["haha"] = responseData;
                    return View();
                }
                return View();
            }
         
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}