using Microsoft.AspNetCore.Authorization;
using Trailblazor.Shared;
using Trailblazor.Shared.ViewModels;

namespace Trailblazor.Server.Handlers.Authorization
{
    public class GearListOwnerHandler : AuthorizationHandler<GearItemOwnerRequirement, GearListViewModel>
    {
        private readonly ILogger<GearListOwnerHandler> _logger;

        public GearListOwnerHandler(ILogger<GearListOwnerHandler> logger)
        {
            _logger = logger;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       GearItemOwnerRequirement requirement,
                                                       GearListViewModel resource)
        {
            // This policy should not be used in an anonymous context, but this is here just in case.
            if (context.User.Identity!.IsAuthenticated)
            {
                if (context.User.GetUserId<Guid>() == resource.OwnerId)
                {
                    context.Succeed(requirement);
                }
                else
                {
                    _logger.LogInformation("User {userId} attempted to modify a record they do not have access to. [documentId: {documentId}, ownerId: {ownerId}]", context.User.UserId(), resource.Id, resource.OwnerId);
                }
            }

            return Task.CompletedTask;
        }
    }

    public record GearItemOwnerRequirement() : IAuthorizationRequirement { }
}
