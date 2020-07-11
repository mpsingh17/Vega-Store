using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VegaStore.Core.Entities;

namespace VegaStore.UI.ViewModels.VehicleViewModels
{
    public class FilterVehicleListViewModel
    {
        public string Color { get; set; }
        public string Condition { get; set; }

        [Display(Name = "Min Price")]
        public decimal? MinPrice { get; set; }
        
        [Display(Name = "Max Price")]
        public decimal? MaxPrice { get; set; }

        [Display(Name = "Is Registered?")]
        public bool IsRegistered { get; set; }
    }
}
