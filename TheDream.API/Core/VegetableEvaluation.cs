﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheDream.API.Models;
using TheDream.DAL.Model;

namespace TheDream.API.Core
{
    public class VegetableEvaluation
    {
        /// <summary>
        /// Get taste value from vegetable
        /// </summary>
        /// <param name="veglist"></param>
        /// <returns></returns>
        public TasteValue VegtableCalculate(List<VegetableDosing> veglist)
        {
            TasteValue VegValue = new TasteValue();

            foreach (VegetableDosing item in veglist)
            {
                VegValue.Salty += item.VegValue.Salty * item.Weight;
                VegValue.Spicy += item.VegValue.Spicy * item.Weight;
                VegValue.Sour += item.VegValue.Sour * item.Weight;
                VegValue.Sweet += item.VegValue.Sweet * item.Weight;
                VegValue.Bitter += item.VegValue.Bitter * item.Weight;
                VegValue.Weight += item.Weight;

            }
            return VegValue;

        }
        /// <summary>
        /// Get the salty and oil value from meat
        /// </summary>
        /// <param name="veglist"></param>
        /// <returns></returns>
        public TasteValue MeatCalculate(List<MeatDosing> MeatList)
        {
            TasteValue MeatValue = new TasteValue();

            foreach (MeatDosing item in MeatList)
            {
                MeatValue.Salty += item.MeatValue.Salty * item.Weight;
                MeatValue.Oil += item.MeatValue.Fat * item.Weight;

                MeatValue.Weight += item.Weight;

            }
            return MeatValue;

        }
        public TasteValue SeasonCalculate(List<SeasonDosing> SeasonList)
        {
            TasteValue SeasonValue = new TasteValue();
            foreach (SeasonDosing item in SeasonList)
            {
                SeasonValue.Salty += item.SeasonValue.Salty * item.Weight;
                SeasonValue.Spicy += item.SeasonValue.Spicy * item.Weight;
                SeasonValue.Sour += item.SeasonValue.Sour * item.Weight;
                SeasonValue.Bitter += item.SeasonValue.Bitter * item.Weight;
                SeasonValue.Sweet += item.SeasonValue.Sweet * item.Weight;
                SeasonValue.Oil += item.SeasonValue.Bitter * item.Weight;
                SeasonValue.Weight = item.Weight;
            }
            return SeasonValue;
        }
    }
}