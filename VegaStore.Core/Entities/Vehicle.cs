using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace VegaStore.Core.Entities
{
    public enum Colors
    {
        Red = 1, Green, Black
    }

    public enum Condition
    {
        New = 1, Old
    }

    public class Vehicle : EntityBase
    {
        public string Name { get; set; }

        public bool IsRegistered { get; set; }

        public Colors Color { get; set; }

        public Condition Condition { get; set; }

        public decimal Price { get; set; }

        public int ModelId { get; set; }
        public Model Model { get; set; }

        public ICollection<VehicleFeature> VehicleFeatures { get; set; } = new Collection<VehicleFeature>();

    }
}
