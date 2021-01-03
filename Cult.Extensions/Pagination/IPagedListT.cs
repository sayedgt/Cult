using System.Collections.Generic;

namespace Cult.Extensions.Pagination
{
    public interface IPagedList<T> : IPagedList
    {
        new ICollection<T> Items { get; }
    }
}
