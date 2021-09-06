using EnergyAccountManagement.DataAccess.Contracts;
using EnergyAccountManagement.DataAccess.Entities;
using EnergyAccountManagement.Service.Contracts;
using EnergyAccountManagement.Service.Dto;
using EnergyAccountManagement.Service.Implementation;
using EnergyAccountManagement.Service.Mapping;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnergyAccountManagement.Service.Tests
{
    public class AccountServiceTests
    {
        private Mock<IAccountDataAccess> mockAcountDataAccess;

        private Mock<IAccountMapper> mockAccountMapper;

        [SetUp]
        public void SetUp()
        {
            mockAcountDataAccess = new Mock<IAccountDataAccess>();

            mockAccountMapper = new Mock<IAccountMapper>();
        }

        [TestCase(1223)]
        public async Task DeleteAsync_Returns_Successfully_If_Account_Exists(int accountId)
        {
            mockAcountDataAccess.Setup(x => x.DeleteAsync(accountId)).Returns(Task.CompletedTask);

            IAccountService accountService = new AccountService(mockAcountDataAccess.Object, mockAccountMapper.Object);

            var response = await accountService.DeleteAsync(accountId);

            Assert.That(response.IsSuccessful, Is.EqualTo(true));
        }

        [TestCase(0001)]
        public void DeleteAsync_Throws_Exception_If_Account_Does_NOT_Exist(int accountId)
        {
            mockAcountDataAccess.Setup(x => x.DeleteAsync(accountId)).ThrowsAsync(new Exception("Account does not exist"));

            IAccountService accountService = new AccountService(mockAcountDataAccess.Object, mockAccountMapper.Object);

            var exception = Assert.ThrowsAsync<Exception>(async () => await accountService.DeleteAsync(accountId));

            Assert.That(exception.Message, Is.EqualTo("Account does not exist"));
        }

        [Test]
        public async Task GetAllAsync_Returns_Data_If_Accounts_Exist()
        {
            var accounts = new List<Account>
            {
                new Account{ AccountId=1232, LastName="Test Last Name 1", FirstName="Test First Name 1" },
                new Account{ AccountId=1233, LastName="Test Last Name 2", FirstName="Test First Name 2" },
                new Account{ AccountId=1234, LastName="Test Last Name 3", FirstName="Test First Name 3" }
            };

            mockAcountDataAccess.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(accounts));

            mockAccountMapper.Setup(x => x.Map(accounts)).Returns(new List<AccountDto>
            {
                new AccountDto{ AccountId=1232, LastName="Test Last Name 1", FirstName="Test First Name 1" },
                new AccountDto{ AccountId=1233, LastName="Test Last Name 2", FirstName="Test First Name 2" },
                new AccountDto{ AccountId=1234, LastName="Test Last Name 3", FirstName="Test First Name 3" }
            });

            IAccountService accountService = new AccountService(mockAcountDataAccess.Object, mockAccountMapper.Object);

            var response = await accountService.GetAllAsync();

            var accountDtos = (List<AccountDto>)response.Data;

            Assert.That(response.IsSuccessful, Is.EqualTo(true));

            Assert.That(accountDtos.Count, Is.EqualTo(accounts.Count));
        }

        [Test]
        public async Task GetAllAsync_Does_NOT_Return_Data_If_Accounts_Do_NOT_Exist()
        {
            var accounts = new List<Account>();

            mockAcountDataAccess.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(accounts));

            mockAccountMapper.Setup(x => x.Map(accounts)).Returns(new List<AccountDto>());

            IAccountService accountService = new AccountService(mockAcountDataAccess.Object, mockAccountMapper.Object);

            var response = await accountService.GetAllAsync();

            var accountDtos = (List<AccountDto>)response.Data;

            Assert.That(response.IsSuccessful, Is.EqualTo(true));

            Assert.That(accountDtos, Is.Empty);
        }

        [Test]
        public async Task GetAsync_Returns_Data_If_Account_Exists()
        {
            var accountId = 1234;

            var account = new Account { AccountId = accountId, LastName = "Test Last Name 3", FirstName = "Test First Name 3" };

            mockAcountDataAccess.Setup(x => x.GetAsync(accountId)).Returns(Task.FromResult(account));

            mockAccountMapper.Setup(x => x.Map(account)).Returns(
                new AccountDto { AccountId = accountId, LastName = "Test Last Name 3", FirstName = "Test First Name 3" }
            );

            IAccountService accountService = new AccountService(mockAcountDataAccess.Object, mockAccountMapper.Object);

            var response = await accountService.GetAsync(accountId);

            var accountDto = (AccountDto)response.Data;

            Assert.That(response.IsSuccessful, Is.EqualTo(true));

            Assert.That(accountDto.AccountId, Is.EqualTo(accountId));
        }

        [Test]
        public async Task GetAsync_Does_NOT_Return_Data_If_Account_Does_NOT_Exist()
        {
            var accountId = 0091;

            Account account = null;

            AccountDto accountDto = null;

            mockAcountDataAccess.Setup(x => x.GetAsync(accountId)).Returns(Task.FromResult(account));

            mockAccountMapper.Setup(x => x.Map(account)).Returns(accountDto);

            IAccountService accountService = new AccountService(mockAcountDataAccess.Object, mockAccountMapper.Object);

            var response = await accountService.GetAsync(accountId);

            Assert.That(response.IsSuccessful, Is.EqualTo(true));

            Assert.That(response.Data, Is.Null);
        }

        [Test]

        public async Task UpdateAsync_Returns_Successfully_If_Account_Exists()
        {
            var accountId = 1234;

            var accountDto = new AccountDto { AccountId = accountId, LastName = "Test Last Name 3", FirstName = "Test First Name 3" };

            var account = new Account { AccountId = accountId, LastName = "Test Last Name 3", FirstName = "Test First Name 3" };

            mockAcountDataAccess.Setup(x => x.UpdateAsync(accountId, account)).Returns(Task.CompletedTask);

            mockAccountMapper.Setup(x => x.Map(accountDto)).Returns(account);

            IAccountService accountService = new AccountService(mockAcountDataAccess.Object, mockAccountMapper.Object);

            var response = await accountService.UpdateAsync(accountId, accountDto);

            Assert.That(response.IsSuccessful, Is.EqualTo(true));

            Assert.That(response.Error, Is.Null);
        }

        [Test]
        public void UpdateAsync_Throws_Exception_If_Account_Does_NOT_Exist()
        {
            var accountId = 1234;

            var accountDto = new AccountDto { AccountId = accountId, LastName = "Test", FirstName = "Test" };

            var account = new Account { AccountId = accountId, LastName = "Test", FirstName = "Test" };

            mockAcountDataAccess.Setup(x => x.UpdateAsync(accountId, account)).ThrowsAsync(new Exception("Account does not exist"));

            mockAccountMapper.Setup(x => x.Map(accountDto)).Returns(account);

            IAccountService accountService = new AccountService(mockAcountDataAccess.Object, mockAccountMapper.Object);

            var exception = Assert.ThrowsAsync<Exception>(async () => await accountService.UpdateAsync(accountId, accountDto));

            Assert.That(exception.Message, Is.EqualTo("Account does not exist"));
        }

        [Test]

        public async Task SaveAsync_Returns_Successfully()
        {
            var accountId = 1111;

            var accountDto = new AccountDto { AccountId = accountId, LastName = "Nike", FirstName = "Test" };

            var account = new Account { AccountId = accountId, LastName = "Nike", FirstName = "Test" };

            mockAcountDataAccess.Setup(x => x.SaveAsync(account)).Returns(Task.CompletedTask);

            mockAccountMapper.Setup(x => x.Map(accountDto)).Returns(account);

            IAccountService accountService = new AccountService(mockAcountDataAccess.Object, mockAccountMapper.Object);

            var response = await accountService.SaveAsync(accountDto);

            mockAcountDataAccess.Verify(x => x.SaveAsync(account), Times.Once);

            Assert.That(response.IsSuccessful, Is.EqualTo(true));

            Assert.That(response.Error, Is.Null);
        }
    }
}
