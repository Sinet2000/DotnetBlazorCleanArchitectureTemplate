using Microsoft.AspNetCore.Authorization;
using PaperStop.Shared.Constants.Permission;

namespace Server.Permission;

internal class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    public PermissionAuthorizationHandler()
    { }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        if (context.User == null)
        {
            await Task.CompletedTask;
        }

        var permissions = context.User.Claims.Where(x => x.Type == ApplicationClaimTypes.Permission &&
                                                         x.Value == requirement.Permission &&
                                                         x.Issuer == "LOCAL AUTHORITY");
        if (permissions.Any())
        {
            context.Succeed(requirement);
            await Task.CompletedTask;
        }
    }
}
