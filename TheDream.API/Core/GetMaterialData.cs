using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheDream.DAL.DAL;
using TheDream.DAL.Model;
using TheDream.Model.Models;

namespace TheDream.API.Core
{
    public class GetMaterialData
    {
        public Recipe TransData(RecipeViewModel model)
        {
            using (Cloud_ChefEntities context = new Cloud_ChefEntities())
            {
                Recipe OrderRecipe = new Recipe();
                if (model.VegViewDosings != null)
                {
                    foreach (VegViewDosing item in model.VegViewDosings)
                    {
                        VegetableDosing VegDosing = new VegetableDosing();
                        VegDosing.VegValue = context.Vegetable.Where(x => x.Active == true && x.VegId == item.Id).Select(x => x).FirstOrDefault();
                        VegDosing.Weight = item.Weight;
                        OrderRecipe.VegDosing.Add(VegDosing);
                    }
                }
                if (model.MeatViewDosings != null)
                {
                    foreach (MeatViewDosing item in model.MeatViewDosings)
                    {
                        MeatDosing MeatDosings = new MeatDosing();
                        MeatDosings.MeatValue = context.Meat.Where(x => x.Active == true && x.MeatId == item.Id).Select(x => x).FirstOrDefault();
                        MeatDosings.Weight = item.Weight;
                        OrderRecipe.MeatDosings.Add(MeatDosings);
                    }
                }
                if (model.EggViewDosings != null)
                {
                    foreach (EggViewDosing item in model.EggViewDosings)
                    {
                        EggDosing EggDosings = new EggDosing();
                        EggDosings.EggValue = context.Egg.Where(x => x.Active == true && x.EggId == item.Id).Select(x => x).FirstOrDefault();
                        EggDosings.Number = item.Number;
                        OrderRecipe.EggDosings.Add(EggDosings);
                    }
                }
                if (model.SeasonViewDosings != null)
                {
                    foreach (SeasonViewDosing item in model.SeasonViewDosings)
                    {
                        SeasonDosing SeasonDosings = new SeasonDosing();
                        SeasonDosings.SeasonValue = context.Season.Where(x => x.Active == true && x.SeasonId == item.Id).Select(x => x).FirstOrDefault();
                        SeasonDosings.Weight = item.Weight;
                        OrderRecipe.SeasonDosings.Add(SeasonDosings);
                    }
                }
                OrderRecipe.Time = model.Time;
                OrderRecipe.Type = model.Type;
                return OrderRecipe;

            }


        }
    }
}