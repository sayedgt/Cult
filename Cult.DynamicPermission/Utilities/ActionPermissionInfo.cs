using System.Collections.Generic;
using System;

namespace Cult.DynamicPermission.Utilities
{
    public class ActionPermissionInfo
    {
        public string Name { get; set; }
        public List<PermissionInfo> Permissions { get; set; }
    }
}