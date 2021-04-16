using System.Linq;
using System.Threading.Tasks;
using Cult.DynamicPermission;
using Cult.DynamicPermission.Requirements;
using Microsoft.AspNetCore.Authorization;
// ReSharper disable All

namespace DynamicPermission.Requirements
{
    public class PermissionRequirementHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var permissions = context.User.Claims.Where(
                x => x.Type == DynamicPermissionConstants.PolicyType
                &&
                x.Value == requirement.Permission);

            if (permissions.Any())
            {
                context.Succeed(requirement);
                await Task.CompletedTask.ConfigureAwait(false);
            }
        }
    }
}