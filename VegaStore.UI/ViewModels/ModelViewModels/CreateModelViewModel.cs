﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VegaStore.UI.ViewModels.ModelViewModels
{
    public class CreateModelViewModel
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name = "Select Make")]
        public int MakeId { get; set; }
        public IEnumerable<SelectListItem> MakeSLIs { get; set; }
    }
}
