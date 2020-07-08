using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VegaStore.Core.RequestFeatures
{
    public class PagedList<T> : List<T>
    {
        public int ItemsCount { get; set; }

        public PagedList(IEnumerable<T> items, int count)
        {
            ItemsCount = count;
            AddRange(items);
        }

        public static PagedList<T> ToPagedList(IQueryable<T> source, int start, int length)
        {
            var count = source.Count();

            var items = source.Skip(start).Take(length).ToList();

            return new PagedList<T>(items, count);
        }
    }
}
