using System;
using Microsoft.AspNetCore.Authorization;

namespace Cult.DynamicPermission.Requirements
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement(string permission)
        {
            Permission = permission ?? throw new ArgumentNullException(nameof(permission));
        }

        public string Permission { get; }
    }
}