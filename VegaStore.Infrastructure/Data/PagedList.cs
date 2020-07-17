using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VegaStore.Infrastructure.Data
{
    public class PagedList<T> : List<T>
    {
        public int ItemsCount { get; set; }

        public PagedList(IEnumerable<T> items, int count)
        {
            ItemsCount = count;
            AddRange(items);
        }

        public async static Task<PagedList<T>> ToPagedList(IQueryable<T> source, int start, int length)
        {
            var count = source.Count();

            var items = await source.Skip(start).Take(length).ToListAsync();

            return new PagedList<T>(items, count);
        }
    }
}
