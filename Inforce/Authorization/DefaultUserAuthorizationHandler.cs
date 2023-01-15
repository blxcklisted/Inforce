using Inforce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace Inforce.Authorization;

public class DefaultUserAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, UrlShortener>
{
    private readonly UserManager<IdentityUser> _userManager;

    public DefaultUserAuthorizationHandler(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        OperationAuthorizationRequirement requirement,
        UrlShortener shortener)
    {
        if (context.User == null || shortener == null)
            return Task.CompletedTask;

        if (requirement.Name != Constants.CreateOperationName &&
            requirement.Name != Constants.ReadOperationName &&
            requirement.Name != Constants.UpdateOperationName &&
            requirement.Name != Constants.DeleteOperationName)
        {
            return Task.CompletedTask;
        }

        if (shortener.CreatorId == _userManager.GetUserId(context.User))
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}

