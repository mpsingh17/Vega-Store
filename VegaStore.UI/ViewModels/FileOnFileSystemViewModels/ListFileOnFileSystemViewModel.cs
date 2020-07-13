using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VegaStore.UI.ViewModels.FileOnFileSystemViewModels
{
    public class ListFileOnFileSystemViewModel
    {
        public int Id { get; set; }

        public string Path { get; set; }

        [Display(Name = "Uploaded At")]
        public string CreatedAt { get; set; }
    }
}
