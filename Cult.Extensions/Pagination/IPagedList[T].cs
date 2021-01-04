using System.Collections.Generic;
// ReSharper disable once CheckNamespace

namespace Cult.Extensions
{
    public interface IPagedList<T> : IPagedList
    {
        new ICollection<T> Items { get; }
    }
}
