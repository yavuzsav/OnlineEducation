using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OnlineEducation.Entities.Identity;

namespace OnlineEducation.DataAccess.Concrete.SeedData
{
    public class AppRoleSeed
    {
        public static async Task SeedData(RoleManager<AppRole> roleManager)
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
