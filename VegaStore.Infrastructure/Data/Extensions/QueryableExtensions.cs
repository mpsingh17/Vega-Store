using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace VegaStore.Infrastructure.Data.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, int skip, int take)
        {
            return query.Skip(skip).Take(take);
        }
    }
}
