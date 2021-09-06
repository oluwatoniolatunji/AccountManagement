using EnergyAccountManagement.DataAccess.Entities;
using EnergyAccountManagement.Service.Dto;
using System.Collections.Generic;
using System.Linq;

namespace EnergyAccountManagement.Service.Mapping
{
    public class AccountMapper : IAccountMapper
    {
        public List<AccountDto> Map(List<Account> accounts)
            => (from account in accounts
                select new AccountDto
                {
                    AccountId = account.AccountId,
                    FirstName = account.FirstName,
                    LastName = account.LastName
                }).ToList();

        public Account Map(AccountDto accountDto)
            => new()
            {
                AccountId = accountDto.AccountId,
                FirstName = accountDto.FirstName,
                LastName = accountDto.LastName
            };

        public AccountDto Map(Account account)
            => account == null ? null : new()
            {
                AccountId = account.AccountId,
                FirstName = account.FirstName,
                LastName = account.LastName
            };
    }
}
