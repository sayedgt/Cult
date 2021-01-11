using Cult.Extensions.ExtraArray;
using System.Data;
// ReSharper disable All 
namespace Cult.Extensions.ExtraConnectionState
{
    public static class ConnectionStateExtensions
    {
        public static bool In(this ConnectionState @this, params ConnectionState[] values)
        {
            return values.IndexOf(@this) != -1;
        }
        public static bool NotIn(this ConnectionState @this, params ConnectionState[] values)
        {
            return values.IndexOf(@this) == -1;
        }
    }
}
