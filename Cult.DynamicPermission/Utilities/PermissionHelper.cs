using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cult.DynamicPermission.Attributes;
using Cult.Mvc.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace Cult.DynamicPermission.Utilities
{
    public static class PermissionHelper
    {
        public static IEnumerable<ControllerPermissionInfo> GetPermissions(Assembly assembly = null)
        {
            var result = new List<ControllerPermissionInfo>();
            var data = MvcUtilities.GetControllersInfo(assembly);

            foreach (var item in data)
            {
                var attrs = item.Attributes.Union(item.Actions.SelectMany(x => x.Attributes)).Distinct().ToList();
                if (attrs.Any(y => y is AuthorizeAttribute))
                {
                    var permissions = attrs
                        .Where(x => x is AuthorizeAttribute attribute && !string.IsNullOrEmpty(attribute.Policy))
                        .Select(x => new PermissionInfo
                        {
                            Name = ((AuthorizeAttribute)x).Policy,
                            Description = x is not PermissionAttribute ? null : ((PermissionAttribute)x).Description
                        });

                    var currentController = result.FirstOrDefault(x => x.Namespace == item.Namespace && x.Name == item.Name);

                }
            }

            return result;
        }
    }
}
