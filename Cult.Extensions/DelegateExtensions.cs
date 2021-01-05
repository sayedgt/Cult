using System;
// ReSharper disable All 
namespace Cult.Extensions
{
    public static class DelegateExtensions
    {
        public static Delegate Combine(this Delegate a, Delegate b)
        {
            return Delegate.Combine(a, b);
        }
        public static Delegate Remove(this Delegate source, Delegate value)
        {
            return Delegate.Remove(source, value);
        }
        public static Delegate RemoveAll(this Delegate source, Delegate value)
        {
            return Delegate.RemoveAll(source, value);
        }
    }
}
