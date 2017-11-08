using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheDream.API.Models
{
    public class TasteValue
    {
        public double? Salty { get; set; }
        public double? Spicy { get; set; }
        public double? Sour { get; set; }
        public double? Bitter { get; set; }
        public double? Sweet { get; set; }
        public double? Oil { get; set; }
        public int Weight { get; set; }
    }
}