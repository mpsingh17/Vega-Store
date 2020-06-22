﻿using System;
using System.Collections.Generic;
using System.Text;

namespace VegaStore.Core.Entities
{
    public class VehicleFeature
    {
        public int FeatureId { get; set; }
        public int VehicleId { get; set; }

        public Feature Feature { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
