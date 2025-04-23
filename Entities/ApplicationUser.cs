using Microsoft.AspNetCore.Identity;

namespace Fintelex_Assignment.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Role { get; set; } = "User"; // Default role
    }
}
