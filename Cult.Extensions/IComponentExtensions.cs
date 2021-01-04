using System.ComponentModel;
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global

namespace Cult.Extensions
{
    public static class IComponentExtensions
    {
        public static bool IsInDesignMode(this IComponent target)
        {
            var site = target.Site;
            return site != null && site.DesignMode;
        }
        public static bool IsInRuntimeMode(this IComponent target)
        {
            return !IsInDesignMode(target);
        }
    }
}
