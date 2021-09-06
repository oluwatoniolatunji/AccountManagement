using EnergyAccountManagement.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnergyAccountManagement.DataAccess.Contracts
{
    public interface IAccountDataAccess
    {
        Task SaveAsync(Account accountToSave);
        Task UpdateAsync(Account accountToUpdate);
        Task DeleteAsync(int accountId);
        Task<Account> GetAsync(int accountId);
        Task<List<Account>> GetAllAsync();
    }
}
