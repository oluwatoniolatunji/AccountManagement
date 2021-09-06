using EnergyAccountManagement.DataAccess.Contracts;
using EnergyAccountManagement.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;

namespace EnergyAccountManagement.DataAccess.Implementation
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly AccountManagementDbContext accountManagementDbContext;

        public DatabaseInitializer(AccountManagementDbContext accountManagementDbContext)
        {
            this.accountManagementDbContext = accountManagementDbContext;
        }

        public void Initialize()
        {
            accountManagementDbContext.Database.EnsureCreated();

            if (accountManagementDbContext.Accounts.Any()) return;

            var accounts = new List<Account>()
            {
                new() { AccountId = 2344, FirstName = "Tommy", LastName = "Test" },
                new() { AccountId = 2233, FirstName = "Barry", LastName = "Test" },
                new() { AccountId = 8766, FirstName = "Sally", LastName = "Test" },
                new() { AccountId = 2345, FirstName = "Jerry", LastName = "Test" },
                new() { AccountId = 2346, FirstName = "Ollie", LastName = "Test" },
                new() { AccountId = 2347, FirstName = "Tara", LastName = "Test" },
                new() { AccountId = 2348, FirstName = "Tammy", LastName = "Test" },
                new() { AccountId = 2349, FirstName = "Simon", LastName = "Test" },
                new() { AccountId = 2350, FirstName = "Colin", LastName = "Test" },
                new() { AccountId = 2351, FirstName = "Gladys", LastName = "Test" },
                new() { AccountId = 2352, FirstName = "Greg", LastName = "Test" },
                new() { AccountId = 2353, FirstName = "Tony", LastName = "Test" },
                new() { AccountId = 2355, FirstName = "Arthur", LastName = "Test" },
                new() { AccountId = 2356, FirstName = "Craig", LastName = "Test" },
                new() { AccountId = 6776, FirstName = "Laura", LastName = "Test" },
                new() { AccountId = 4534, FirstName = "JOSH", LastName = "TEST" },
                new() { AccountId = 1234, FirstName = "Freya", LastName = "Test" },
                new() { AccountId = 1239, FirstName = "Noddy", LastName = "Test" },
                new() { AccountId = 1240, FirstName = "Archie", LastName = "Test" },
                new() { AccountId = 1241, FirstName = "Lara", LastName = "Test" },
                new() { AccountId = 1242, FirstName = "Tim", LastName = "Test" },
                new() { AccountId = 1243, FirstName = "Graham", LastName = "Test" },
                new() { AccountId = 1244, FirstName = "Tony", LastName = "Test" },
                new() { AccountId = 1245, FirstName = "Neville", LastName = "Test" },
                new() { AccountId = 1246, FirstName = "Jo", LastName = "Test" },
                new() { AccountId = 1247, FirstName = "Jim", LastName = "Test" },
                new() { AccountId = 1248, FirstName = "Pam", LastName = "Test" }
            };

            accountManagementDbContext.Accounts.AddRange(accounts);

            accountManagementDbContext.SaveChanges();
        }
    }
}
