using System;
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
            var categories = new List<Category>();
            if (!context.Categories.Any())
            {
                categories.AddRange(new List<Category>
                {
                    new Category {Id = Guid.NewGuid(), Name = "9.Sınıf", Description = "9.Sınıf"},
                    new Category {Id = Guid.NewGuid(), Name = "10.Sınıf", Description = "10.Sınıf"},
                    new Category {Id = Guid.NewGuid(), Name = "11.Sınıf", Description = "11.Sınıf"},
                    new Category {Id = Guid.NewGuid(), Name = "12.Sınıf", Description = "12.Sınıf"}
                });
                await context.Categories.AddRangeAsync(categories);
            }

            var lessons = new List<Lesson>();
            if (!context.Lessons.Any())
            {
                lessons.AddRange(new List<Lesson>
                {
                    new Lesson
                    {
                        Id = Guid.NewGuid(), Name = "Matematik", Description = "9.sınıf matematik",
                        Category = categories[0]
                    },
                    new Lesson
                    {
                        Id = Guid.NewGuid(), Name = "Edebiyat", Description = "9.sınıf edebiyat",
                        Category = categories[0]
                    },
                    new Lesson
                    {
                        Id = Guid.NewGuid(), Name = "Coğrafya", Description = "9.sınıf coğrafya",
                        Category = categories[0]
                    },
                    new Lesson
                    {
                        Id = Guid.NewGuid(), Name = "Geometri", Description = "9.sınıf geometri",
                        Category = categories[0]
                    },
                    new Lesson
                    {
                        Id = Guid.NewGuid(), Name = "Fizik", Description = "9.sınıf fizik", Category = categories[0]
                    }
                });

                await context.Lessons.AddRangeAsync(lessons);
            }

            await context.SaveChangesAsync();
        }
    }
}
