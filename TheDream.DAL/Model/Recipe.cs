using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheDream.Model.Models;

namespace TheDream.DAL.Model
{
   public  class Recipe
    {
        public List<VegetableDosing> VegDosing { get; set; }
        public List<MeatDosing> MeatDosings { get; set; }
        public List<SeasonDosing> SeasonDosings { get; set; }
        public List<EggDosing> EggDosings { get; set; }

        public CookType Type { get; set; }
        public int Time { get; set; }
        //time mins
    }

  
}
