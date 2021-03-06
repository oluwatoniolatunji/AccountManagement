using EnergyAccountManagement.Api.Middlewares;
using EnergyAccountManagement.DataAccess;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace EnergyAccountManagement.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureDatabaseServices(services);

            ConfigureDefaultServices(services);
        }

        public virtual void ConfigureDatabaseServices(IServiceCollection services)
        {
            services.AddDbContext<AccountManagementDbContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("AccountManagementDbConnection"),
                   x => x.MigrationsAssembly("EnergyAccountManagement.DataAccess"))
            );
        }

        public void ConfigureDefaultServices(IServiceCollection services)
        {
            services.AddRepositoryDependencies();

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(GlobalExceptionFilter));
            }).AddNewtonsoftJson();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                    .AddFluentValidation();

            services.Configure((Action<ApiBehaviorOptions>)(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    return context.GetModelStateError();
                };
            }));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Account Management APIs",
                    Version = "v1",
                    Description = "APIs for managing customer accounts."
                });
            });
        }

        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./v1/swagger.json", "Account Management APIs");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
