using System;
using System.Collections.Generic;

namespace Cult.Mvc.Utilities
{
    public class ActionInfo
    {
        public string Name { get; set; }
        public Type ActionReturnType { get; set; }
        public ICollection<Attribute> Attributes { get; set; }
    }
}