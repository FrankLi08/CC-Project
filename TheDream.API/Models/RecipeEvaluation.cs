using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheDream.API.Core;
using TheDream.DAL.Model;
using TheDream.Model.Models;

namespace TheDream.API.Models
{
    public class RecipeEvaluation
    {
        public EstimateResult EvaluteRecipe(RecipeViewModel Recipemodel)
        {
            Recipe model = new Recipe();
            GetMaterialData GetData = new GetMaterialData();
            model = GetData.TransData(Recipemodel);

            TasteCalculator calculator = new TasteCalculator();
            List<TasteValue> TasteList = new List<TasteValue>();
            TasteEstimate TasteEst = new TasteEstimate();
            TasteList.Add(calculator.VegtableCalculate(model.VegDosing));
            TasteList.Add(calculator.MeatCalculate(model.MeatDosings));
            TasteList.Add(calculator.SeasonCalculate(model.SeasonDosings));
            TasteList.Add(calculator.EggCalculate(model.EggDosings));
            EstimateResult result = TasteEst.EstimateScore(calculator.AvgCalculator(TasteList), model.VegDosing);
            return result; 
        }
    }
}