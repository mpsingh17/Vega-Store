using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VegaStore.UI.ViewModels.Public.VehicleViewModels
{
    public class PaginatedListVehicleViewModel
    {
        public IEnumerable<ListVehicleViewModel> Vehicles { get; set; }

        public int TotalVehicles { get; set; }
        public int Length { get; set; } = 2;
        public int Start { get; set; } = 1;

        public bool HasPrevious
        {
            get
            {
                return Start > 0;
            }
        }

        public bool HasNext
        {
            get
            {
                return (Start + Length) < TotalVehicles;
            }
        }
    }
}
