#nullable enable

using System;
using Microsoft.AspNetCore.Authorization;
// ReSharper disable UnusedMember.Global
// ReSharper disable RedundantAttributeUsageProperty

namespace Cult.DynamicPermission.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class PermissionAttribute : AuthorizeAttribute
    {
        public PermissionAttribute() { }
        public PermissionAttribute(string policy) : base(policy) { }
        public PermissionAttribute(string policy, string description) : base(policy)
        {
            Description = description;
        }
        public string? Description { get; set; }
    }
}