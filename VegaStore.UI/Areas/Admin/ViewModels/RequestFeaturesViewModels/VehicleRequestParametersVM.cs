using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VegaStore.UI.Areas.Admin.ViewModels.RequestFeaturesViewModels
{
    public class VehicleRequestParametersVM : RequestFeaturesViewModel
    {
        public string Color { get; set; }
        public string Condition { get; set; }

        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
