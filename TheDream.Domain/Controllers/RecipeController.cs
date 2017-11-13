using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TheDream.Domain.Repository.Interface;
using TheDream.Model.Models;

namespace TheDream.Domain.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IHttpClientRepo _ClientRepo;

        public RecipeController(IHttpClientRepo ClientRepo)
        {
            _ClientRepo = ClientRepo;
        }

        // GET: Recipe
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(RecipeViewModel model)
        {
            HttpResponseMessage Response = await _ClientRepo.SendRecipe(model);
            var result = Response.Content.ReadAsStringAsync().Result;
            var estimateResult = JsonConvert.DeserializeObject<List<EstimateResult>>(result);
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> GetVegList()
        {
            HttpResponseMessage response = await _ClientRepo.GetVegList();
            var result = response.Content.ReadAsStringAsync().Result;
            List<VegetableList> VegListResult = JsonConvert.DeserializeObject<List<VegetableList>>(result);
            return PartialView(VegListResult);
        }
        [HttpGet]
        public async Task<ActionResult> GetMeatList()
        {
            HttpResponseMessage response = await _ClientRepo.GetMeatList();
            var result = response.Content.ReadAsStringAsync().Result;
            List<MeatList> MeatListResult = JsonConvert.DeserializeObject<List<MeatList>>(result);
            return PartialView(MeatListResult);
        }
        [HttpGet]
        public async Task<ActionResult> GetSeasonList()
        {
            HttpResponseMessage response = await _ClientRepo.GetSeasonList();
            var result = response.Content.ReadAsStringAsync().Result;
            List<SeasonList> SeasonListResult = JsonConvert.DeserializeObject<List<SeasonList>>(result);
            return PartialView(SeasonListResult);
        }
        [HttpGet]
        public async Task<ActionResult> GetEggList()
        {
            HttpResponseMessage response = await _ClientRepo.GetSeasonList();
            var result = response.Content.ReadAsStringAsync().Result;
            List<EggList> EggListResult = JsonConvert.DeserializeObject<List<EggList>>(result);
            return PartialView(EggListResult);
        }
    }
}