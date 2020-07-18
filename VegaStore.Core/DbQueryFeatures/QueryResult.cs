using System;
using System.Collections.Generic;
using System.Text;

namespace VegaStore.Core.DbQueryFeatures
{
    public class QueryResult<T>
    {
        public int ItemCount { get; set; }
        public IEnumerable<T> Items { get; set; }

    }
}
