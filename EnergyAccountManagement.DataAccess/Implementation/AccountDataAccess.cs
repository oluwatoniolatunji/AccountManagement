using EnergyAccountManagement.DataAccess.Contracts;
using EnergyAccountManagement.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnergyAccountManagement.DataAccess.Implementation
{
    public class AccountDataAccess : IAccountDataAccess
    {
        private readonly AccountManagementDbContext accountManagementDbContext;

        public AccountDataAccess(AccountManagementDbContext accountManagementDbContext)
        {
            this.accountManagementDbContext = accountManagementDbContext;
        }

        public async Task DeleteAsync(int accountId)
        {
            var account = await accountManagementDbContext.Accounts.FirstOrDefaultAsync(t => t.AccountId == accountId);

            if (account == null) throw new Exception("Account does not exist");

            accountManagementDbContext.Remove(account);

            await accountManagementDbContext.SaveChangesAsync();
        }

        public async Task<List<Account>> GetAllAsync()
        {
            return await accountManagementDbContext.Accounts.AsNoTracking().ToListAsync();
        }

        public async Task<Account> GetAsync(int accountId)
        {
            return await accountManagementDbContext.Accounts.AsNoTracking().FirstOrDefaultAsync(a => a.AccountId == accountId);
        }

        public async Task SaveAsync(Account accountToSave)
        {
            accountManagementDbContext.Accounts.Add(accountToSave);

            await accountManagementDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(int accountId, Account accountToUpdate)
        {
            var account = await accountManagementDbContext.Accounts.FirstOrDefaultAsync(t => t.AccountId == accountId);

            if (account == null) throw new Exception("Account does not exist");

            account.FirstName = accountToUpdate.FirstName;

            account.LastName = accountToUpdate.LastName;

            accountManagementDbContext.Entry(account).State = EntityState.Modified;

            await accountManagementDbContext.SaveChangesAsync();
        }
    }
}
