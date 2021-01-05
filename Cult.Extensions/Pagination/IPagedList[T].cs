using System.Collections.Generic;
// ReSharper disable once CheckNamespace

// ReSharper disable All 
namespace Cult.Extensions
{
    public interface IPagedList<T> : IPagedList
    {
        new ICollection<T> Items { get; }
    }
}
