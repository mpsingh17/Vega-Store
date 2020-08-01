using System.Collections.Generic;
using VegaStore.UI.Areas.Admin.ViewModels.RequestFeaturesViewModels;

namespace VegaStore.UI.ViewModels.VehicleViewModels
{
    public class PublicListVehicleViewModel
    {
        public IEnumerable<VehicleViewModel> Vehicles { get; set; }

        public VehicleParametersViewModel VehicleParametersVM { get; set; }
    }
}
