using EnergyAccountManagement.Service.Dto;
using EnergyAccountManagement.Service.Validators;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace EnergyAccountManagement.Service.Tests
{
    public class AccountValidatorTests
    {
        private AccountValidator validator;

        [SetUp]
        public void SetUp()
        {
            validator = new AccountValidator();
        }

        [Test]
        public void Should_Not_Have_Any_Errors()
        {
            var model = new AccountDto { FirstName = "Test 1", LastName = "Test 1", AccountId = 1222 };

            var result = validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void Should_Have_Error_When_Account_Id_Is_Not_Provided()
        {
            var model = new AccountDto { FirstName = "Test", LastName = "Test" };

            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(a => a.AccountId)
                .WithErrorMessage("Account Id is required.");
        }

        [Test]
        public void Should_Have_Error_When_LastName_Is_Not_Provided()
        {
            var model = new AccountDto { FirstName = "Test", AccountId = 1222 };

            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(a => a.LastName)
                .WithErrorMessage("Last name is required.");
        }

        [Test]
        public void Should_Have_Error_When_LastName_Is_More_Than_50_Characters()
        {
            var model = new AccountDto
            {
                FirstName = "Test",
                AccountId = 1222,
                LastName = "Krhoqmphovggqapoxltginpxhhcdulogevxwkmdyjmgamxqmimu"
            };

            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(a => a.LastName)
                .WithErrorMessage("Last name must not exceed 50 characters.");
        }

        [Test]
        public void Should_Have_Error_When_FirstName_Is_Not_Provided()
        {
            var model = new AccountDto { LastName = "Test Last Name", AccountId = 1222 };

            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(a => a.FirstName)
                .WithErrorMessage("First name is required.");
        }

        [Test]
        public void Should_Have_Error_When_FirstName_Is_More_Than_50_Characters()
        {
            var model = new AccountDto
            {
                FirstName = "Gnicszlkgurveobevqrrejl uhbsmvjedymgsotdupwpulqaiirz",
                AccountId = 1222,
                LastName = "Last Name 1"
            };

            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(a => a.FirstName)
                .WithErrorMessage("First name must not exceed 50 characters.");
        }
    }
}
