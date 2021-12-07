using Microsoft.AspNetCore.Identity;

namespace Trailblazor.Server.Models;

public class ApplicationUser : IdentityUser
{
    public string ImageUrl { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}
