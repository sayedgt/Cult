using System.Collections;
// ReSharper disable once CheckNamespace

// ReSharper disable All 
namespace Cult.Extensions
{
    public interface IPagedList
    {
        ICollection Items { get; }
        int Count { get; }
        int PageIndex { get; }
        int PageSize { get; }
        int TotalCount { get; }
        int TotalPages { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
    }
}
