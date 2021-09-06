using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EnergyAccountManagement.DataAccess.Tests.Helpers
{
    public class DbContextHelper
    {
        public static async Task<AccountManagementDbContext> GetDbContext(bool seedData = true)
        {
            var builder = new DbContextOptionsBuilder<AccountManagementDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var dbContext = new AccountManagementDbContext(builder.Options);

            if (seedData)
            {
                await dbContext.SeedDataAsync();
            }

            return dbContext;
        }
    }
}
