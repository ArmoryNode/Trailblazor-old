using Microsoft.AspNetCore.Authorization;
using Trailblazor.Shared;
using Trailblazor.Shared.ViewModels;

namespace Trailblazor.Server.Handlers.Authorization
{
    public class GearItemOwnerHandler : AuthorizationHandler<GearItemOwnerRequirement, GearItemViewModel>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       GearItemOwnerRequirement requirement,
                                                       GearItemViewModel resource)
        {
            // This policy should not be used in an anonymous context, but this is here just in case.
            if (context.User.Identity!.IsAuthenticated)
            {
                if (context.User.GetUserId<Guid>() == resource.OwnerId)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }

    public record GearItemOwnerRequirement() : IAuthorizationRequirement { }
}
