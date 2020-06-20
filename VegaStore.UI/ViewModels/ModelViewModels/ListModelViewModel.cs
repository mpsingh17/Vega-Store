using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VegaStore.UI.ViewModels.ModelViewModels
{
    public class ListModelViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Make { get; set; }
        public string CreatedAt { get; set; }
    }
}
