using EnergyAccountManagement.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnergyAccountManagement.DataAccess.Tests.Helpers
{
    public static class TestDataHelper
    {
        public static async Task SeedDataAsync(this AccountManagementDbContext dbContext)
        {
            var seedData = GetTestAccounts();

            await dbContext.Accounts.AddRangeAsync(seedData);

            await dbContext.SaveChangesAsync();
        }

        public static List<Account> GetTestAccounts()
            => new()
            {
                new() { AccountId = 1122, FirstName = "First Name 1", LastName = "Last Name 1" },
                new() { AccountId = 2322, FirstName = "First Name 2", LastName = "Last Name 2" },
                new() { AccountId = 2772, FirstName = "First Name 3", LastName = "Last Name 3" },
                new() { AccountId = 2312, FirstName = "First Name 4", LastName = "Last Name 4" },
                new() { AccountId = 4533, FirstName = "First Name 5", LastName = "Last Name 5" },
                new() { AccountId = 2367, FirstName = "First Name 6", LastName = "Last Name 6" },
                new() { AccountId = 4345, FirstName = "First Name 7", LastName = "Last Name 7" }
            };
    }
}
