using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VegaStore.UI.ViewModels.FeatureViewModels
{
    public class SaveFeatureViewModel
    {

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }

}
