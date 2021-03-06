﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VegaStore.Core.Entities;

namespace VegaStore.UI.Areas.Admin.ViewModels.VehicleViewModels
{
    public class ListVehicleViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Registered")]
        public string IsRegistered { get; set; }

        public string Color { get; set; }

        public string Condition { get; set; }

        public string Price { get; set; }

        public string Model { get; set; }

        public string CreatedAt { get; set; }

        public IEnumerable<LinkViewModel> Links { get; set; }

    }
}
