using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDream.Model.Models
{
   public class RecipeViewModel
    {
        public RecipeViewModel()
        {
            this.VegViewDosings = new List<VegViewDosing>();
            this.MeatViewDosings = new List<MeatViewDosing>();
            this.SeasonViewDosings = new List<SeasonViewDosing>();
            this.EggViewDosings = new List<EggViewDosing>();
            this.Time = 0;
        }

        public List<VegViewDosing> VegViewDosings { get; set; }
        public List<MeatViewDosing> MeatViewDosings { get; set; }
        public List<SeasonViewDosing> SeasonViewDosings { get; set; }
        public List<EggViewDosing> EggViewDosings { get; set; }
        public CookType Type { get; set; }
        public int Time { get; set; }


    }
    public class VegViewDosing
    {
        public VegViewDosing()
        {
            this.Id = 0;
        }
        public string  Name { get; set; }
        public int Id { get; set; }
        public int Weight { get; set; }
    }
    public class MeatViewDosing
    {
        public MeatViewDosing()
        {
            this.Id = 0;
        }
        public string Name { get; set; }
        public int Id { get; set; }
        public int Weight { get; set; }
    }
    public class EggViewDosing
    {
        public EggViewDosing()
        {
            this.Id = 0;
        }
        public string Name { get; set; }
        public int Id { get; set; }
        public int Number { get; set; }
    }
    public class SeasonViewDosing
    {
        public SeasonViewDosing()
        {
            this.Id = 0;
        }
        public string Name { get; set; }
        public int Id { get; set; }
        public int Weight { get; set; }
    }
    public enum CookType { fry, deepfry}
   
}
