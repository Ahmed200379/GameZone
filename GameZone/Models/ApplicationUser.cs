using Microsoft.AspNetCore.Identity;

namespace GameZone.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Country { get; set; }
    }
}
