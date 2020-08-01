using System;
using System.Collections.Generic;
using System.Text;
using VegaStore.Core.Entities;

namespace VegaStore.Core.DbQueryFeatures
{
    public class VehicleQueryParameters : QueryParameters
    {
        public string Color { get; set; }
        public string Condition { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

    }
}
