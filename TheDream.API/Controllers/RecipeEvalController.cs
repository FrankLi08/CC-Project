using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TheDream.API.Models;
using TheDream.DAL.DAL;
using TheDream.DAL.Model;
using TheDream.Model.Models;

namespace TheDream.API.Controllers
{   [Authorize]
    [RoutePrefix("api/RecipeEval")]
    public class RecipeEvalController : ApiController
    {
      
        [Route("Recipe")]
        public HttpResponseMessage EstimateRecipe([FromBody] RecipeViewModel model)
        {
            if(model!=null)
            {
                RecipeEvaluation Evaluate = new RecipeEvaluation();
                return Request.CreateResponse(HttpStatusCode.OK, Evaluate.EvaluteRecipe(model));
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [Route("VegetableList")]
        public HttpResponseMessage GetVegetableList()
        {
            using (Cloud_ChefEntities context = new Cloud_ChefEntities())
            {
                List<VegetableList> VegList = context.Vegetable.Where(x => x.Active == true).Select(x => new VegetableList { Id = x.VegId, VegName = x.Name }).ToList();
                if (VegList.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, VegList);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
        }


        [Route("MeatList")]
        public HttpResponseMessage GetMeatList()
        {
            using (Cloud_ChefEntities context = new Cloud_ChefEntities())
            {
                List<MeatList> MeatLists = context.Meat.Where(x => x.Active == true).Select(x => new MeatList { Id = x.MeatId, MeatName = x.Name }).ToList();
                if (MeatLists.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, MeatLists);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
        }

        [Route("SeasonList")]
        public HttpResponseMessage GetSeasonList()
        {
            using (Cloud_ChefEntities context = new Cloud_ChefEntities())
            {
                List<SeasonList> SeasonLists = context.Season.Where(x => x.Active == true).Select(x => new SeasonList { Id = x.SeasonId, SeasonName = x.Name }).ToList();
                if (SeasonLists.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, SeasonLists);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
        }

        [Route("EggList")]
        public HttpResponseMessage GetEggList()
        {
            using (Cloud_ChefEntities context = new Cloud_ChefEntities())
            {
                List<EggList> EggLists = context.Egg.Where(x => x.Active == true).Select(x => new EggList { Id = x.EggId, EggName = x.Name }).ToList();
                if (EggLists.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, EggLists);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
        }
    }
}
