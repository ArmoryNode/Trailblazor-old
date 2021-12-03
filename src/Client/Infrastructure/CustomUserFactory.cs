using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using System.Security.Claims;
using static Trailblazor.Shared.Infrastructure.Authentication;

namespace Trailblazor.Client.Infrastructure
{
    public class CustomUserFactory : AccountClaimsPrincipalFactory<RemoteUserAccount>
    {
        public CustomUserFactory(IAccessTokenProviderAccessor accessor) : base(accessor)
        {
        }

        public async override ValueTask<ClaimsPrincipal> CreateUserAsync(RemoteUserAccount account, RemoteAuthenticationUserOptions options)
        {
            var user = await base.CreateUserAsync(account, options);

            if (user.Identity!.IsAuthenticated)
            {
                var identity = user.Identity as ClaimsIdentity;

                string? value = identity!.FindFirst(CustomClaimTypes.Image)?.Value;
            }

            return user;
        }
    }
}
