using EnergyAccountManagement.Service.Contracts;
using EnergyAccountManagement.Service.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnergyAccountManagement.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost("SaveAccount")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponseDto))]
        public async Task<IActionResult> SaveAccountAsync([FromBody] AccountDto accountToSave)
        {
            var response = await accountService.SaveAsync(accountToSave);

            return Ok(response);
        }


        [HttpPost("UpdateAccount")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponseDto))]
        public async Task<IActionResult> UpdateAccountAsync([FromBody] AccountDto accountToUpdate)
        {
            var response = await accountService.UpdateAsync(accountToUpdate);

            return Ok(response);
        }

        [HttpGet("GetAccounts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces(typeof(IEnumerable<AccountDto>))]
        public async Task<IActionResult> GetAccountsAsync()
        {
            var response = await accountService.GetAllAsync();

            return Ok(response);
        }

        [HttpGet("GetAccount/{accountId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces(typeof(AccountDto))]
        public async Task<IActionResult> GetAccountAsync(int accountId)
        {
            var response = await accountService.GetAsync(accountId);

            return Ok(response);
        }
    }
}
