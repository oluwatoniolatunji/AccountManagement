using System;
using EnergyAccountManagement.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace EnergyAccountManagement.Api.Integration.Tests.Utilities
{
    public class CustomWebApplicationFactory : BaseWebApplicationFactory<TestStartup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                services.AddDbContext<AccountManagementDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryAppDb");
                        options.UseInternalServiceProvider(serviceProvider);
                    });
            });
        }
    }
}
