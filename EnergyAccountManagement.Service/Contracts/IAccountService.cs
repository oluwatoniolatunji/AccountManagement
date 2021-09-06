using EnergyAccountManagement.Service.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnergyAccountManagement.Service.Contracts
{
    public interface IAccountService
    {
        Task<ApiResponseDto> SaveAsync(AccountDto accountToSave);
        Task<ApiResponseDto> UpdateAsync(int accountId, AccountDto accountToUpdate);
        Task<ApiResponseDto> DeleteAsync(int accountId);
        Task<ApiResponseDto> GetAsync(int accountId);
        Task<ApiResponseDto> GetAllAsync();
    }
}
