using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

            return services;
        }
    }
}
