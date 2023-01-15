using Inforce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Inforce.Authorization;

public class AdministratorAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, UrlShortener>
{
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

        if (context.User.IsInRole(Constants.ShortenerAdministratorRole))
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}

