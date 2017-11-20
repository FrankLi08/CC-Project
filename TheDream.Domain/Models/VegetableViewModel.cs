using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheDream.Domain.Models
{
    public class VegetableViewModel
    {
        public int VegetableId { get; set; }
        public IEnumerable<SelectListItem> VegetableList { get; set; }
        public int Weight { get; set; }
    }
}