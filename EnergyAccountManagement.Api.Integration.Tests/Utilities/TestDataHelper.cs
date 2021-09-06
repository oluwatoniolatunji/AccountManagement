using EnergyAccountManagement.DataAccess;
using EnergyAccountManagement.DataAccess.Entities;
using System.Collections.Generic;

namespace EnergyAccountManagement.Api.Integration.Tests.Utilities
{
    public class TestDataHelper
    {
        private readonly AccountManagementDbContext dbContext;

        public TestDataHelper(AccountManagementDbContext dbContext)
        {
            this.dbContext = dbContext;

            this.dbContext.Database.EnsureDeleted();

            this.dbContext.Database.EnsureCreated();
        }

        public void PopulateTestData()
        {
            var seedData = new List<Account>
            {
                new() { AccountId = 1222, FirstName = "Name 1", LastName = "Last Name 1" },
                new() { AccountId = 2122, FirstName = "Name 2", LastName = "Last Name 2" },
                new() { AccountId = 2972, FirstName = "Name 3", LastName = "Last Name 3" },
                new() { AccountId = 2146, FirstName = "Name 4", LastName = "Last Name 4" },
                new() { AccountId = 4511, FirstName = "Name 5", LastName = "Last Name 5" },
                new() { AccountId = 2355, FirstName = "Name 6", LastName = "Last Name 6" }
            };

            dbContext.Accounts.AddRange(seedData);

            dbContext.SaveChanges();
        }
    }
}
