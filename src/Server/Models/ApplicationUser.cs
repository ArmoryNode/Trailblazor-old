using Microsoft.AspNetCore.Identity;

namespace Trailblazor.Server.Models;

public class ApplicationUser : IdentityUser
{
    public string ImageUrl { get; set; } = string.Empty;
}
