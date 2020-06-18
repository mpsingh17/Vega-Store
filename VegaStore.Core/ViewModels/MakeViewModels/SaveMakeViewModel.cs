using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VegaStore.Core.ViewModels.MakeViewModels
{
    public class SaveMakeViewModel
    {
        [Required]
        [MaxLength(255, ErrorMessage = "Name field can exceed 255 character limit.")]
        public string Name { get; set; }
    }
}
