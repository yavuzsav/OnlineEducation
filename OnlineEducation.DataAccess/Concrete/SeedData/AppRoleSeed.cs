using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OnlineEducation.Entities.Identity;

namespace OnlineEducation.DataAccess.Concrete.SeedData
{
    public class AppRoleSeed
    {
        public static async Task SeedDataAsync(RoleManager<AppRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                var roles = new List<AppRole>
                {
                    new AppRole {Name = "Student"},
                    new AppRole {Name = "Teacher"},
                    new AppRole {Name = "Admin"}
                };

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }
            }
        }
    }
}
