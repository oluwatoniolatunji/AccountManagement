using EnergyAccountManagement.Service.Contracts;
using EnergyAccountManagement.Service.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnergyAccountManagement.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/account-management")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost("accounts")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponseDto))]
        public async Task<IActionResult> SaveAccountAsync(AccountDto accountToSave)
        {
            var response = await accountService.SaveAsync(accountToSave);

            return Ok(response);
        }


        [HttpPost("accounts/{accountId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponseDto))]
        public async Task<IActionResult> UpdateAccountAsync(int accountId, AccountDto accountToUpdate)
        {
            var response = await accountService.UpdateAsync(accountId, accountToUpdate);

            return Ok(response);
        }

        [HttpGet("accounts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces(typeof(IEnumerable<AccountDto>))]
        public async Task<IActionResult> GetAccountsAsync()
        {
            var response = await accountService.GetAllAsync();

            return Ok(response);
        }

        [HttpGet("accounts/{accountId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces(typeof(AccountDto))]
        public async Task<IActionResult> GetAccountAsync(int accountId)
        {
            var response = await accountService.GetAsync(accountId);

            return Ok(response);
        }
    }
}
