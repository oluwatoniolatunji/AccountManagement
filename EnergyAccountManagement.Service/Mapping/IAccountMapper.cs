using EnergyAccountManagement.DataAccess.Entities;
using EnergyAccountManagement.Service.Dto;
using System.Collections.Generic;

namespace EnergyAccountManagement.Service.Mapping
{
    public interface IAccountMapper
    {
        List<AccountDto> Map(List<Account> accounts);
        Account Map(AccountDto accountToSave);
        AccountDto Map(Account account);
    }
}
