using System.Collections.Generic;
using VegaStore.UI.Areas.Admin.ViewModels.RequestFeaturesViewModels;
using VegaStore.UI.ViewModels.RequestFeatureViewModels;

namespace VegaStore.UI.ViewModels.VehicleViewModels
{
    public class PublicListVehicleViewModel
    {
        public IEnumerable<VehicleViewModel> Vehicles { get; set; }

        public PaginationDetails PaginationDetails { get; set; }
    }
}
