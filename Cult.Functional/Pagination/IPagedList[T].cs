using System.Collections.Generic;
// ReSharper disable All 
namespace Cult.Functional
{
    public interface IPagedList<T> : IPagedList
    {
        new ICollection<T> Items { get; }
    }
}
