using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VegaStore.UI.ViewModels.RequestFeatureViewModels
{
    public class PublicRequestParametersVM
    {
        private const int _maxLength = 2;

        private int _length = _maxLength;
        public int Length
        {
            get => _length;
            set => _length = (value > _maxLength) ? _maxLength : value;
        }

        public int Start { get; set; }

        public int TotalItemsCount { get; set; }
        public bool HasPrevious => Start > 0;
        public bool HasNext => (Start + Length) < TotalItemsCount;
    }
}
