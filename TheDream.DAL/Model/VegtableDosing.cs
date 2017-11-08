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
            this.VegValue = new Vegetable();
            this.Weight = 0;
        }
        public Vegetable VegValue { get; set; }
        public int Weight { get; set; }

    }
    public class MeatDosing
    {
        public MeatDosing()
        {
            this.MeatValue = new Meat();
            this.Weight = 0;
            
        }
        public Meat MeatValue { get; set; }
        public int Weight { get; set; }
    }
    public class SeasonDosing
    {
        public SeasonDosing()
        {
            this.SeasonValue = new Season();
            this.Weight = 0;
        }
        public Season SeasonValue { get; set; }
        public int Weight { get; set; }

    }
    public class EggDosing
    {
        public EggDosing()
        {
            this.EggValue = new Egg();
            this.Number = 0;

        }
        public Egg EggValue { get; set; }
        public int Number { get; set; }
    }

}
