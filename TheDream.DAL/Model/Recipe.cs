using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDream.DAL.Model
{
   public  class Recipe
    {
        public List<VegtableDosing> VegDosing { get; set; }
        public CookType MyProperty { get; set; }
        public int Time { get; set; }
        //time mins
    }

    public enum CookType { fry,deepfry }
}
