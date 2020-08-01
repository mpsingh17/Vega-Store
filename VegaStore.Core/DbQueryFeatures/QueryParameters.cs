using System;
using System.Collections.Generic;
using System.Text;

namespace VegaStore.Core.DbQueryFeatures
{
    public class QueryParameters
    {
        private const int _maxPageSize = 50;
        private const int _defaultPageSize = 10;
        private const int _defaultPageNumber = 1;

        private int _pageSize = _defaultPageSize;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > _maxPageSize) ? _maxPageSize : value;
        }

        public int PageNumber { get; set; } = _defaultPageNumber;

        public string SearchTerm { get; set; }

    }
}
