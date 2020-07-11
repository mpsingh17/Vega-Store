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

        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
