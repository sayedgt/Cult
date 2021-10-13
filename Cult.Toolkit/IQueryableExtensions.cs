using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Cult.Toolkit.ExtraIQueryable
{
    public static class IQueryableExtensions
    {
        public static bool Any<T>(this IQueryable<T> source)
        {
            return source.Count() > 0;
        }

        public static bool Any<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate)
        {
            return source.Count(predicate) > 0;
        }

        public static IEnumerable<TEntity> ToPaged<TEntity>(this IQueryable<TEntity> query, int pageIndex, int pageSize)
        {
            return query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                ;
        }
    }
}
