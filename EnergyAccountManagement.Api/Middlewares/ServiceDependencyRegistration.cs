using EnergyAccountManagement.DataAccess.Contracts;
using EnergyAccountManagement.DataAccess.Implementation;
using EnergyAccountManagement.Service.Contracts;
using EnergyAccountManagement.Service.Implementation;
using EnergyAccountManagement.Service.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace EnergyAccountManagement.Api.Middlewares
{
    public static class ServiceDependencyRegistration
    {
        public static IServiceCollection AddRepositoryDependencies(this IServiceCollection services)
        {
            services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();

            services.AddTransient<IAccountDataAccess, AccountDataAccess>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IAccountMapper, AccountMapper>();

            return services;
        }
    }
}
