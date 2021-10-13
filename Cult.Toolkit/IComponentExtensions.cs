﻿using System.ComponentModel;

namespace Cult.Toolkit.ExtraIComponent
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
