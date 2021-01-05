using System.Collections.Generic;
// ReSharper disable All 
namespace Cult.NetDiff
{
    public class DiffOption<T>
    {
        public IEqualityComparer<T> EqualityComparer { get; set; }
        public int Limit { get; set; }
    }
}
