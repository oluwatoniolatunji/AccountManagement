using EnergyAccountManagement.DataAccess.Contracts;
using EnergyAccountManagement.Service.Contracts;
using EnergyAccountManagement.Service.Dto;
using EnergyAccountManagement.Service.Mapping;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnergyAccountManagement.Service.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly IAccountDataAccess accountDataAccess;

        private readonly IAccountMapper accountMapper;

        public AccountService(IAccountDataAccess accountDataAccess, IAccountMapper accountMapper)
        {
            this.accountDataAccess = accountDataAccess;
            this.accountMapper = accountMapper;
        }

        public async Task<ApiResponseDto> DeleteAsync(int accountId)
        {
            await accountDataAccess.DeleteAsync(accountId);

            return new ApiResponseDto();
        }

        public async Task<ApiResponseDto> GetAllAsync()
        {
            var accounts = await accountDataAccess.GetAllAsync();

            var accountDtos = accountMapper.Map(accounts);

            return new ApiResponseDto { Data = accountDtos };
        }

        public async Task<ApiResponseDto> GetAsync(int accountId)
        {
            var account = await accountDataAccess.GetAsync(accountId);

            var accountDto = accountMapper.Map(account);

            return new ApiResponseDto { Data = accountDto };
        }

        public async Task<ApiResponseDto> SaveAsync(AccountDto accountToSave)
        {
            var account = accountMapper.Map(accountToSave);

            await accountDataAccess.SaveAsync(account);

            return new ApiResponseDto();
        }

        public async Task<ApiResponseDto> UpdateAsync(int accountId, AccountDto accountToUpdate)
        {
            var account = accountMapper.Map(accountToUpdate);

            await accountDataAccess.UpdateAsync(accountId, account);

            return new ApiResponseDto();
        }
    }
}
