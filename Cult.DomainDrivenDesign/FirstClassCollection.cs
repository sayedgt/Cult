using System.Collections.Generic;
using System.Collections.ObjectModel;
// ReSharper disable All
namespace Cult.DomainDrivenDesign
{
    public class FirstClassCollection<T>
    {
        private readonly IList<T> _collection;

        public FirstClassCollection()
        {
            _collection = new List<T>();
        }

        public void Add(IEnumerable<T> collection)
        {
            foreach (var item in collection)
                Add(item);
        }

        public void Add(T item)
        {
            _collection.Add(item);
        }


        public void Remove(IEnumerable<T> collection)
        {
            foreach (var item in collection)
                Remove(item);
        }

        public void Remove(T item)
        {
            _collection.Remove(item);
        }

        public IReadOnlyCollection<T> Get()
        {
            return new ReadOnlyCollection<T>(_collection);
        }
    }
}
