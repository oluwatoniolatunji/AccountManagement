using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EnergyAccountManagement.Api.Integration.Tests.Utilities
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void ConfigureDatabaseServices(IServiceCollection services)
        {
            services.AddTransient<TestDataHelper>();
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            base.Configure(app, env);

            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            
            var testDataHelper = serviceScope.ServiceProvider.GetService<TestDataHelper>();
            
            testDataHelper.PopulateTestData();
        }
    }
}