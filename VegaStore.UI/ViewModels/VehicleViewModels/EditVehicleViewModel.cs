using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VegaStore.Core.Entities;
using VegaStore.UI.ValidationAttributes;

namespace VegaStore.UI.ViewModels.VehicleViewModels
{
    public class EditVehicleViewModel
    {
        [Required]
        [Display(Name = "Model")]
        public int ModelId { get; set; }
        public IEnumerable<SelectListItem> ModelSLIs { get; set; } = new List<SelectListItem>();

        [Required]
        [Display(Name = "Features")]
        public ICollection<int> FeatureIds { get; set; } = new Collection<int>();
        public IEnumerable<SelectListItem> FeatureSLIs { get; set; }

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


        public string CurrentFeaturedImagePath { get; set; }
        
        [MaxFileSize(1 * 1024 * 1024)]
        [AllowedFileExtensions(new string[] { ".jpg", ".png" })]
        public IFormFile FeaturedImage { get; set; }
    }
}
