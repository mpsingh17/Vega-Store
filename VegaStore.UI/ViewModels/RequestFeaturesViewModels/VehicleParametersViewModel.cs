using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VegaStore.UI.ViewModels.RequestFeaturesViewModels
{
    public class VehicleParametersViewModel : RequestFeaturesViewModel
    {
        public string Color { get; set; }
        public string Condition { get; set; }

        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
