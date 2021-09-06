using EnergyAccountManagement.DataAccess.Contracts;
using EnergyAccountManagement.DataAccess.Entities;
using EnergyAccountManagement.DataAccess.Implementation;
using EnergyAccountManagement.DataAccess.Tests.Helpers;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace EnergyAccountManagement.DataAccess.Tests
{
    public class AccountDataAccessTests
    {
        [Test]

        public async Task SaveAsync_Saves_Account_Successfully()
        {
            //Arrange
            var accountId = 5667;

            var dbContext = await DbContextHelper.GetDbContext();

            IAccountDataAccess accountDataAccess = new AccountDataAccess(dbContext);

            var accountToSave = new Account { AccountId = accountId, FirstName = "Mike", LastName = "Test" };

            //Act
            await accountDataAccess.SaveAsync(accountToSave);

            //Assert
            var accountSaved = await dbContext.Accounts.FirstOrDefaultAsync(a => a.AccountId == accountId);

            Assert.That(accountSaved.AccountId, Is.EqualTo(accountId));

            Assert.That(accountSaved.FirstName, Is.EqualTo("Mike"));

            Assert.That(accountSaved.LastName, Is.EqualTo("Test"));
        }

        [TestCase(1122)]
        [TestCase(4533)]
        public async Task DeleteAsync_Deletes_Account_If_Account_Exists(int accountId)
        {
            //Arrange
            var dbContext = await DbContextHelper.GetDbContext();

            IAccountDataAccess accountDataAccess = new AccountDataAccess(dbContext);

            //Act
            await accountDataAccess.DeleteAsync(accountId);

            //Assert
            var accountDeleted = await dbContext.Accounts.FirstOrDefaultAsync(a => a.AccountId == accountId);

            Assert.That(accountDeleted, Is.Null);
        }

        [TestCase(0000)]
        public async Task DeleteAsync_Throws_Exception_If_Account_Does_NOT_Exist(int accountId)
        {
            //Arrange
            var dbContext = await DbContextHelper.GetDbContext();

            IAccountDataAccess accountDataAccess = new AccountDataAccess(dbContext);

            //Act
            var exception = Assert.ThrowsAsync<Exception>(async () => await accountDataAccess.DeleteAsync(accountId));

            //Assert
            Assert.That(exception.Message, Is.EqualTo("Account does not exist"));
        }

        [Test]

        public async Task UpdateAsync_Updates_Account_If_Account_Exists()
        {
            //Arrange
            var dbContext = await DbContextHelper.GetDbContext();

            IAccountDataAccess accountDataAccess = new AccountDataAccess(dbContext);

            var accountToUpdate = new Account { AccountId = 2367, FirstName = "Graham", LastName = "Test" };

            //Act
            await accountDataAccess.UpdateAsync(accountToUpdate);

            //Assert
            var accountUpdated = await dbContext.Accounts.FirstOrDefaultAsync(a => a.AccountId == 2367);

            Assert.That(accountUpdated.FirstName, Is.EqualTo("Graham"));

            Assert.That(accountUpdated.LastName, Is.EqualTo("Test"));
        }

        [Test]
        public async Task UpdateAsync_Throws_Exception_If_Account_Does_NOT_Exist()
        {
            //Arrange
            var dbContext = await DbContextHelper.GetDbContext();

            IAccountDataAccess accountDataAccess = new AccountDataAccess(dbContext);

            var accountToUpdate = new Account { AccountId = 9999, FirstName = "Tony", LastName = "Test" };

            //Act
            var exception = Assert.ThrowsAsync<Exception>(async () => await accountDataAccess.UpdateAsync(accountToUpdate));

            //Assert
            Assert.That(exception.Message, Is.EqualTo("Account does not exist"));
        }

        [Test]
        public async Task GetAsync_Gets_Account_If_Account_Exists()
        {
            //Arrange
            var dbContext = await DbContextHelper.GetDbContext();

            IAccountDataAccess accountDataAccess = new AccountDataAccess(dbContext);

            //Act
            var account = await accountDataAccess.GetAsync(2322);

            //Assert
            Assert.That(account.FirstName, Is.EqualTo("First Name 2"));

            Assert.That(account.LastName, Is.EqualTo("Last Name 2"));
        }

        [Test]
        public async Task GetAsync_Does_NOT_Return_Account_If_It_Does_NOT_Exist()
        {
            //Arrange
            var dbContext = await DbContextHelper.GetDbContext();

            IAccountDataAccess accountDataAccess = new AccountDataAccess(dbContext);

            //Act
            var account = await accountDataAccess.GetAsync(0999);

            //Assert
            Assert.That(account, Is.Null);
        }

        [Test]
        public async Task GetAllAsync_Returns_Data_If_Accounts_Exist()
        {
            //Arrange
            var dbContext = await DbContextHelper.GetDbContext();

            IAccountDataAccess accountDataAccess = new AccountDataAccess(dbContext);

            //Act
            var accounts = await accountDataAccess.GetAllAsync();

            //Assert
            Assert.That(accounts, Is.Not.Empty);
        }

        [Test]
        public async Task GetAllAsync_Does_NOT_Return_Data_If_Accounts_Do_NOT_Exist()
        {
            //Arrange
            var dbContext = await DbContextHelper.GetDbContext(seedData: false);

            IAccountDataAccess accountDataAccess = new AccountDataAccess(dbContext);

            //Act
            var accounts = await accountDataAccess.GetAllAsync();

            //Assert
            Assert.That(accounts, Is.Empty);
        }
    }
}