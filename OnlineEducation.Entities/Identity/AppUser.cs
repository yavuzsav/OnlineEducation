using Microsoft.AspNetCore.Identity;

namespace OnlineEducation.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
