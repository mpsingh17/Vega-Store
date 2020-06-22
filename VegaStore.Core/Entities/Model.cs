using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace VegaStore.Core.Entities
{
    public class Model : EntityBase
    {
        public string Name { get; set; }

        public int MakeId { get; set; }
        public Make Make { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; } = new Collection<Vehicle>();
    }
}
