using System;
using System.Collections.Generic;
using System.Text;

namespace VegaStore.Core.Entities
{
    public class VehicleImage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsFeatured { get; set; }

        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

    }
}
