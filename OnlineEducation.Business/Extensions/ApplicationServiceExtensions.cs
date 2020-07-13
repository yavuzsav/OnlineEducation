using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineEducation.Core.Interfaces;
using OnlineEducation.Core.Security;
using OnlineEducation.Core.Services;
using OnlineEducation.DataAccess.Concrete.EntityFramework;
using OnlineEducation.DataAccess.Interfaces;

namespace OnlineEducation.Business.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<OnlineEducationContext>(x =>
            {
                x.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(EfGenericRepository<>));
            services.AddScoped<IUnitOfWork, EfUnitOfWork>();
            services.AddScoped<IUserAccessor, UserAccessor>();
            services.AddScoped<IVideoService, CloudinaryVideoService>();

            return services;
        }
    }
}
