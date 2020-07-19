using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VegaStore.UI.ViewModels.RequestFeaturesViewModels
{
    public class RequestFeaturesViewModel
    {
        private const int _maxPageSize = 10;

        private int _pageLength = _maxPageSize;
        public int Length
        {
            get => _pageLength;
            set => _pageLength = (value > _maxPageSize) ? _maxPageSize : value;
        }

        public int Draw { get; set; }
        public int Start { get; set; }

        public DTSearch Search { get; set; }
    }
}
