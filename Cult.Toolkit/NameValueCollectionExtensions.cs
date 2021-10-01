using System.Collections.Generic;
using System.Collections.Specialized;
// ReSharper disable All 
namespace Cult.Toolkit.ExtraNameValueCollection
{
    public static class NameValueCollectionExtensions
    {
        public static IDictionary<string, object> ToDictionary(this NameValueCollection @this)
        {
            var dict = new Dictionary<string, object>();

            if (@this == null) return dict;

            foreach (var key in @this.AllKeys)
            {
                dict.Add(key, @this[key]);
            }

            return dict;
        }
    }
}
