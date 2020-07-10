using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineEducation.Entities.Entities;
using OnlineEducation.Entities.Identity;

namespace OnlineEducation.DataAccess.Concrete.EntityFramework
{
    public class OnlineEducationContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public OnlineEducationContext(DbContextOptions<OnlineEducationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<ChapterVideo> ChapterVideos { get; set; }
    }
}
