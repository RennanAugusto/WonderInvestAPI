using Microsoft.AspNetCore.Identity;

namespace Wonder.Domain.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FullName { get; set; }
    }
}