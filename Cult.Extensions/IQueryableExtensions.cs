using System.Collections.Generic;
using System.Linq;
namespace Cult.Extensions.ExtraIQueryable
{
    public static class IQueryableExtensions
    {
        public static IEnumerable<TEntity> ToPaged<TEntity>(this IQueryable<TEntity> query, int pageIndex, int pageSize)
        {
            return query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                ;
        }
    }
}
