using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VegaStore.Core.Entities;

namespace VegaStore.Core.RequestFeatures
{
    public class VehicleParameters : RequestParameters
    {
        public string Color { get; set; }
        public string Condition { get; set; }

        public string MinPrice { get; set; }
        public string MaxPrice { get; set; }
    }
}
