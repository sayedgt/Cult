using System;
// ReSharper disable All 
namespace Cult.Extensions
{
    public static class IDisposableExtensions
    {
        public static bool TryDispose(this object toDispose)
        {
            if (!(toDispose is IDisposable disposable))
                return false;

            disposable.Dispose();
            return true;
        }
    }
}
