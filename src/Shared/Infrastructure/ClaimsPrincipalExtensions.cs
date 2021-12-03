using IdentityModel;
using System.ComponentModel;
using System.Security.Claims;

#nullable disable

namespace Trailblazor.Shared
{
    public static class ClaimsPrincipalExtensions
    {
        public static string UserId(this ClaimsPrincipal principal)
            => principal.GetUserId<string>();

        public static string Image(this ClaimsPrincipal principal)
            => principal.FindFirst("image")?.Value;

        public static T GetUserId<T>(this ClaimsPrincipal principal)
        {
            if (principal is null || principal.Identity is null || !principal.Identity.IsAuthenticated)
                throw new ArgumentNullException(nameof(principal));

            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId is null)
                throw new Exception($"`{ClaimTypes.NameIdentifier}` claim not found for user: {principal.Identity.Name}");

            if (typeof(T) == typeof(string) ||
                typeof(T) == typeof(int)    ||
                typeof(T) == typeof(long)   ||
                typeof(T) == typeof(Guid))
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));

                return (T)converter.ConvertFromInvariantString(userId);
            }

            throw new InvalidOperationException("User Id is invalid.");
        }
    }
}
