using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VegaStore.UI.ViewModels
{
    public class PaginationDetails
    {
        public bool HasPrevious { get; set; }
        public bool HasNext { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public int TotalItemsCount { get; set; }

    }
}
