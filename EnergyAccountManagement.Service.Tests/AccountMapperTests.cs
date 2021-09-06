using EnergyAccountManagement.DataAccess.Entities;
using EnergyAccountManagement.Service.Dto;
using EnergyAccountManagement.Service.Mapping;
using NUnit.Framework;
using System.Collections.Generic;

namespace EnergyAccountManagement.Service.Tests
{
    public class AccountMapperTests
    {
        private IAccountMapper accountMapper;

        [SetUp]
        public void SetUp()
        {
            accountMapper = new AccountMapper();
        }

        [Test]
        public void Mapper_Maps_List_Of_Entities_To_List_Of_Dtos_Correctly()
        {
            var accounts = new List<Account>
            {
                new Account{Id = 1, AccountId = 1222, FirstName = "Test 1", LastName = "Test 1"},
                new Account{Id = 2,AccountId = 1224, FirstName = "Test 2", LastName = "Test 2"},
                new Account{Id = 3,AccountId = 1225, FirstName = "Test 3", LastName = "Test 3"},
            };

            var accountDtos = accountMapper.Map(accounts);

            Assert.That(accountDtos.Count, Is.EqualTo(accounts.Count));

            Assert.That(accountDtos.GetType(), Is.EqualTo(typeof(List<AccountDto>)));
        }

        [Test]
        public void Mapper_Maps_Dtos_To_Entity_Correctly()
        {
            var accountDto = new AccountDto { AccountId = 1222, FirstName = "Test 3", LastName = "Test 3" };

            var account = accountMapper.Map(accountDto);

            Assert.That(account.AccountId, Is.EqualTo(accountDto.AccountId));

            Assert.That(account.GetType(), Is.EqualTo(typeof(Account)));
        }

        [Test]
        public void Mapper_Maps_Entity_To_Dto_Correctly()
        {
            var account = new Account { Id = 1, AccountId = 1222, FirstName = "Test 1", LastName = "Test 1" };

            var accountDto = accountMapper.Map(account);

            Assert.That(accountDto.AccountId, Is.EqualTo(account.AccountId));

            Assert.That(accountDto.GetType(), Is.EqualTo(typeof(AccountDto)));
        }
    }
}
