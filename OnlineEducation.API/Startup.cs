using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OnlineEducation.API.Middleware;
using OnlineEducation.Business.Extensions;
using OnlineEducation.Business.Handlers.Category.Queries;
using OnlineEducation.Business.Handlers.User;
using OnlineEducation.Business.Handlers.User.Commands;

namespace OnlineEducation.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
                {
                    // var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                    // options.Filters.Add(new AuthorizeFilter(policy));
                })
                .AddFluentValidation(config => { config.RegisterValidatorsFromAssemblyContaining<LoginValidator>(); });

            services.AddAuthenticationService(Configuration);
            services.AddApplicationServices(Configuration);
            services.AddIdentityService();

            services.AddMediatR(typeof(Register.Handler).Assembly);
            services.AddAutoMapper(typeof(GetCategoryWithLessons.Handler));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
            if (env.IsDevelopment())
            {
                // app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
