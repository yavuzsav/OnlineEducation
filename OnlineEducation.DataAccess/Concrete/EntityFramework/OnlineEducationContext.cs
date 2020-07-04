using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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
    }
}
