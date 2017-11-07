using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheDream.DAL.DAL;

namespace TheDream.DAL.Model
{
   public class VegtableDosing
    {
        public VegtableDosing()
        {
            this.Veg = new Vegetable();
            this.Weight = 0;
        }
        public Vegetable Veg { get; set; }
        public int Weight { get; set; }

    }
}
