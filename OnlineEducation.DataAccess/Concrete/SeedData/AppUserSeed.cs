﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OnlineEducation.Entities.Identity;

namespace OnlineEducation.DataAccess.Concrete.SeedData
{
    public class AppUserSeed
    {
        public static async Task SeedDataAsync(UserManager<AppUser> userManager)
        {
            await Students(userManager);
        }

        private static async Task Students(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var students = new List<AppUser>
                {
                    new AppUser {Email = "bob@test.com", UserName = "bob@test.com", EmailConfirmed = true,},
                    new AppUser {Email = "bruce@test.com", UserName = "bruce@test.com", EmailConfirmed = true,},
                    new AppUser {Email = "tom@test.com", UserName = "tom@test.com", EmailConfirmed = true,},
                    new AppUser {Email = "alice@test.com", UserName = "alice@test.com", EmailConfirmed = true,}
                };

                foreach (var student in students)
                {
                    await userManager.CreateAsync(student, "password");
                    await userManager.AddToRoleAsync(student, "Student");
                }

                var teacherUsers = new List<AppUser>
                {
                    new AppUser {Email = "john@test.com", UserName = "john@test.com", EmailConfirmed = true,},
                    new AppUser {Email = "ivy@test.com", UserName = "ivy@test.com", EmailConfirmed = true,},
                };

                foreach (var teacherUser in teacherUsers)
                {
                    await userManager.CreateAsync(teacherUser, "password");
                    await userManager.AddToRoleAsync(teacherUser, "Teacher");
                }

                var adminUsers = new List<AppUser>
                {
                    new AppUser {Email = "luna@test.com", UserName = "luna@test.com", EmailConfirmed = true,},
                    new AppUser {Email = "finn@test.com", UserName = "finn@test.com", EmailConfirmed = true,},
                    new AppUser {Email = "gabriel@test.com", UserName = "gabriel@test.com", EmailConfirmed = true,},
                };

                foreach (var adminUser in adminUsers)
                {
                    await userManager.CreateAsync(adminUser, "password");
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
