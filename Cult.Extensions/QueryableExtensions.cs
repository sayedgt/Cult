using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
// ReSharper disable All 
namespace Cult.Extensions
{
    public static class QueryableExtensions
    {
        public static IEnumerable<TEntity> ToPaged<TEntity>(this IQueryable<TEntity> query, int pageIndex, int pageSize)
        {
            return query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);
        }
        public static PagedList<TEntity> ToPagedList<TEntity>(this IQueryable<TEntity> items, int pageIndex, int pageSize, int totalCount)
        {
            return new PagedList<TEntity>(items.ToList(), pageIndex, pageSize, totalCount);
        }
        public static PagedList<TEntity> ToPagedList<TEntity, TProperty>(this IQueryable<TEntity> items, int pageIndex, int pageSize, PagedSortDirection sortDirection = PagedSortDirection.Ascending, Expression<Func<TEntity, TProperty>> orderByExpression = null)
        {
            if (pageIndex < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageIndex), "The parameter PageIndex for PagedListLite should be greater than 0");
            }
            var num = pageIndex - 1;
            if (orderByExpression != null)
            {
                switch (sortDirection)
                {
                    case PagedSortDirection.Ascending:
                        items = items.OrderBy(orderByExpression);
                        break;
                    case PagedSortDirection.Descending:
                        items = items.OrderByDescending(orderByExpression);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(sortDirection), sortDirection, null);
                }
            }
            var pagedList = new PagedList<TEntity>(items.Skip(num * pageSize).Take(pageSize).ToArray(), pageIndex, pageSize, items.Count());
            if (pageIndex > pagedList.TotalPages)
            {
                return items.ToPagedList(1, pageSize);
            }
            return pagedList;
        }
        public static PagedList<TEntity> ToPagedList<TEntity>(this IQueryable<TEntity> items, int pageIndex, int pageSize)
        {
            if (pageIndex < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageIndex), "The parameter PageIndex for PagedListLite should be greater than 0");
            }
            var num = pageIndex - 1;
            var pagedList = new PagedList<TEntity>(items.Skip(num * pageSize).Take(pageSize).ToArray(), pageIndex, pageSize, items.Count());
            if (pageIndex > pagedList.TotalPages)
            {
                return items.ToPagedList(1, pageSize);
            }
            return pagedList;
        }
    }
}
