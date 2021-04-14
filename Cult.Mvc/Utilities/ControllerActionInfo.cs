using System;
using System.Collections.Generic;

namespace Cult.Mvc.Utilities
{
    internal class ControllerActionInfo
    {
        internal string AreaName { get; set; }
        internal string Namespace { get; set; }
        internal string ControllerName { get; set; }
        internal string ActionName { get; set; }
        internal Type ActionReturnType { get; set; }
        internal IEnumerable<Attribute> ActionAttributes { get; set; }
        internal IEnumerable<Attribute> ControllerAttributes { get; set; }
    }
}