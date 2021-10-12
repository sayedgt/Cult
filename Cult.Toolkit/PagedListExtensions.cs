using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
// ReSharper disable All

namespace Cult.Toolkit.Pagination
{
    public static class PagedListExtensions
    {
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
        public static PagedList<T> ToPagedList<T>(this ICollection<T> items, int pageIndex, int pageSize)
        {
            return items.AsQueryable().ToPagedList(pageIndex, pageSize);
        }
        public static PagedList<TEntity> ToPagedList<TEntity, TProperty>(this ICollection<TEntity> items, int pageIndex, int pageSize, PagedSortDirection sortDirection = PagedSortDirection.Ascending, Expression<Func<TEntity, TProperty>> orderbyExpression = null)
        {
            return items.AsQueryable().ToPagedList(pageIndex, pageSize, sortDirection, orderbyExpression);
        }
        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> items, int pageIndex, int pageSize)
        {
            return items.AsQueryable().ToPagedList(pageIndex, pageSize);
        }
        public static PagedList<TEntity> ToPagedList<TEntity, TProperty>(this IEnumerable<TEntity> items, int pageIndex, int pageSize, PagedSortDirection sortDirection = PagedSortDirection.Ascending, Expression<Func<TEntity, TProperty>> orderbyExpression = null)
        {
            return items.AsQueryable().ToPagedList(pageIndex, pageSize, sortDirection, orderbyExpression);
        }
        public static PagedList<T> ToPagedList<T>(this T[] items, int pageIndex, int pageSize)
        {
            return items.AsQueryable().ToPagedList(pageIndex, pageSize);
        }
        public static PagedList<TEntity> ToPagedList<TEntity, TProperty>(this TEntity[] items, int pageIndex, int pageSize, PagedSortDirection sortDirection = PagedSortDirection.Ascending, Expression<Func<TEntity, TProperty>> orderbyExpression = null)
        {
            return items.AsQueryable().ToPagedList(pageIndex, pageSize, sortDirection, orderbyExpression);
        }

        public static PagedList<T> ToPagedList<T>(this IList<T> items, int pageIndex, int pageSize)
        {
            return items.AsQueryable().ToPagedList(pageIndex, pageSize);
        }
        public static PagedList<TEntity> ToPagedList<TEntity, TProperty>(this IList<TEntity> items, int pageIndex, int pageSize, PagedSortDirection sortDirection = PagedSortDirection.Ascending, Expression<Func<TEntity, TProperty>> orderbyExpression = null)
        {
            return items.AsQueryable().ToPagedList(pageIndex, pageSize, sortDirection, orderbyExpression);
        }

        public static PagedList<T> ToPagedList<T>(this IReadOnlyCollection<T> items, int pageIndex, int pageSize)
        {
            return items.AsQueryable().ToPagedList(pageIndex, pageSize);
        }
        public static PagedList<TEntity> ToPagedList<TEntity, TProperty>(this IReadOnlyCollection<TEntity> items, int pageIndex, int pageSize, PagedSortDirection sortDirection = PagedSortDirection.Ascending, Expression<Func<TEntity, TProperty>> orderbyExpression = null)
        {
            return items.AsQueryable().ToPagedList(pageIndex, pageSize, sortDirection, orderbyExpression);
        }
        public static PagedList<T> ToPagedList<T>(this IReadOnlyList<T> items, int pageIndex, int pageSize)
        {
            return items.AsQueryable().ToPagedList(pageIndex, pageSize);
        }
        public static PagedList<TEntity> ToPagedList<TEntity, TProperty>(this IReadOnlyList<TEntity> items, int pageIndex, int pageSize, PagedSortDirection sortDirection = PagedSortDirection.Ascending, Expression<Func<TEntity, TProperty>> orderbyExpression = null)
        {
            return items.AsQueryable().ToPagedList(pageIndex, pageSize, sortDirection, orderbyExpression);
        }
    }
}