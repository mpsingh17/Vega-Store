using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VegaStore.UI.ViewModels.VehicleViewModels
{
    public class DetailVehicleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IsRegistered { get; set; }
        public string Color { get; set; }
        public string Condition { get; set; }
        public string Price { get; set; }
        public string Model { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public IEnumerable<string> Features { get; set; }

    }
}
