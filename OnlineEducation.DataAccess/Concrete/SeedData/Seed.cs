﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineEducation.DataAccess.Concrete.EntityFramework;
using OnlineEducation.Entities.Entities;

namespace OnlineEducation.DataAccess.Concrete.SeedData
{
    public class Seed
    {
        public static async Task SeedDataAsync(OnlineEducationContext context)
        {
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category {Id = Guid.NewGuid(), Name = "9.Sınıf", Description = "9.Sınıf"},
                    new Category {Id = Guid.NewGuid(), Name = "10.Sınıf", Description = "10.Sınıf"},
                    new Category {Id = Guid.NewGuid(), Name = "11.Sınıf", Description = "11.Sınıf"},
                    new Category {Id = Guid.NewGuid(), Name = "12.Sınıf", Description = "12.Sınıf"}
                };

                await context.Categories.AddRangeAsync(categories);
            }

            await context.SaveChangesAsync();
        }
    }
}
