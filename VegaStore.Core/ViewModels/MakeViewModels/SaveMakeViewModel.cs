using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VegaStore.Core.ViewModels.MakeViewModels
{
    public class SaveMakeViewModel
    {
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
    }
}
