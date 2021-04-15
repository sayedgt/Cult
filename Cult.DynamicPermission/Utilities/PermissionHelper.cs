using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Cult.DynamicPermission.Attributes;
using Cult.Mvc.Utilities;
using Microsoft.AspNetCore.Authorization;
// ReSharper disable UnusedMember.Global

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
                    var currentController = result.FirstOrDefault(x => x.Namespace == item.Namespace && x.Name == item.Name);
                    var ctrlAttrs = item.Attributes.Where(y =>
                            y is AuthorizeAttribute attribute && !string.IsNullOrEmpty(attribute.Policy))
                        .Select(z => new PermissionInfo()
                        {
                            Name = ((AuthorizeAttribute)z).Policy,
                            Description = z is PermissionAttribute attribute ? attribute.Description : null
                        }).OrderBy(o => o.Description).ThenBy(p => p.Name).ToList();

                    var actions = item.Actions.Select(x => new ActionPermissionInfo()
                    {
                        Name = x.Name,
                        Permissions = x.Attributes.Where(y =>
                                y is AuthorizeAttribute attribute && !string.IsNullOrEmpty(attribute.Policy))
                            .Select(z => new PermissionInfo()
                            {
                                Name = ((AuthorizeAttribute)z).Policy,
                                Description = z is PermissionAttribute attribute ? attribute.Description : null
                            }).Union(ctrlAttrs).OrderBy(o => o.Description).ThenBy(p => p.Name).ToList()
                    });
                    if (currentController != null)
                    {
                        if (!currentController.Actions.Any())
                            currentController.Actions = actions.OrderBy(x => x.Name).ToList();
                        else
                            currentController.Actions.AddRange(actions);
                    }
                    else
                    {
                        var ctrl = new ControllerPermissionInfo
                        {
                            Namespace = item.Namespace,
                            Name = item.Name,
                            AreaName = item.AreaName,
                            Description = item.Attributes.Any(c => c is DisplayAttribute)
                                ? (item.Attributes.First(c => c is DisplayAttribute) as DisplayAttribute)?.Name
                                : null,
                            Actions = actions.OrderBy(x => x.Name).ToList()
                        };
                        result.Add(ctrl);
                    }
                }
            }

            return result.OrderBy(o => o.Description).ThenBy(p => p.Name).ToList();
        }
    }
}
