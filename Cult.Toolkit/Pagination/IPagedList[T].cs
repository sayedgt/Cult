using System.Collections.Generic;
// ReSharper disable All 
namespace Cult.Toolkit.Pagination
{
    public interface IPagedList<T> : IPagedList
    {
        new ICollection<T> Items { get; }
    }
}
