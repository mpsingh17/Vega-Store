using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VegaStore.UI.Areas.Admin.ViewModels.RequestFeaturesViewModels
{
    public class RequestFeaturesViewModel
    {
        private const int _maxPageSize = 2;

        private int _length = _maxPageSize;
        public int Length
        {
            get => _length;
            set => _length = (value > _maxPageSize) ? _maxPageSize : value;
        }

        public int Draw { get; set; }
        public int Start { get; set; }

        public int TotalItemsCount { get; set; }

        public bool HasPrevious => Start > 0;

        public bool HasNext => (Start + Length) < TotalItemsCount;

        public DTSearch Search { get; set; }
    }
}
