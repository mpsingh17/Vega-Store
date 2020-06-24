﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VegaStore.Core.Entities;

namespace VegaStore.UI.ViewModels.VehicleViewModels
{
    public class CreateVehicleViewModel
    {
        [Required]
        [Display(Name = "Model")]
        public int ModelId { get; set; }
        public IEnumerable<SelectListItem> ModelSLIs { get; set; } = new List<SelectListItem>();

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Is Registered")]
        public bool IsRegistered { get; set; }

        [Required]
        public string Price { get; set; }

        [Required]
        [Display(Name = "Color")]
        public Colors Color { get; set; }

        [Required]
        [Display(Name = "Condition")]
        public Condition Condition { get; set; }

    }
}