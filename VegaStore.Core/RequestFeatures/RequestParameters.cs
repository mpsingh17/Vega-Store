﻿using System;
using System.Collections.Generic;
using System.Text;

namespace VegaStore.Core.RequestFeatures
{
    public class RequestParameters
    {
        private const int _maxPageSize = 2;

        private int _pageSize = _maxPageSize;
        public int Length
        {
            get => _pageSize;
            set => _pageSize = (value > _maxPageSize) ? _maxPageSize : value;
        }

        public int Draw { get; set; }
        public int Start { get; set; }

        public string Value { get; set; }
        public bool Regex { get; set; }

    }
}
