using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VegaStore.UI.ViewModels.RequestFeaturesViewModels;

namespace VegaStore.UI.ViewModels.Public.VehicleViewModels
{
    public class ListVehicleViewModel
    {
        public IEnumerable<VehicleViewModel> Vehicles { get; set; }

        public VehicleParametersViewModel VehicleParametersVM { get; set; }
    }
}
