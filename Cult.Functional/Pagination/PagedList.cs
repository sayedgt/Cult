using System.Collections;
using System.Collections.Generic;
// ReSharper disable All 
namespace Cult.Functional
{
    public class PagedList<T> : IPagedList<T>
    {
        public ICollection<T> Items { get; }
        public int Count => Items.Count;
        public int PageIndex { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public int TotalPages => (TotalCount - 1) / PageSize + 1;
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => TotalPages > PageIndex;
        ICollection IPagedList.Items => (ICollection)Items;
        public PagedList(ICollection<T> items, int pageIndex, int pageSize, int totalCount)
        {
            Items = items;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
        }
    }
}
