using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.Api.Domain.Aggregators;
using Sample.Api.Infrastructure;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Api.Application.UserCases.CloseAccount
{
    [Route("api/v1/accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly ILogger<AccountsController> _logger;
        private readonly IMongoBaseRepository<Account> _accountRepository;


        public AccountsController(
            ILogger<AccountsController> logger,
            IMongoBaseRepository<Account> accountRepository)
        {
            _logger = logger;
            _accountRepository = accountRepository;
        }


        /// <summary>
        ///     Close the account
        /// </summary>
        [HttpDelete("{accountId}")]
        [SwaggerResponse(200, "The account was deleted", typeof(Account))]
        [SwaggerResponse(404, "The account not found")]
        [SwaggerResponse(400, "The account not be closed")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> Close(Guid accountId)
        {
            var source = new CancellationTokenSource();

            var token = source.Token;

            _logger.LogInformation($"Get Account by {accountId}");

            var account = await _accountRepository.GetByIdAsync(accountId, token);

            if (account is null)
            {
                _logger.LogWarning("Account not found !");
                return NotFound();
            }

            account.CanBeClosed();

            await _accountRepository.DeleteAsync(accountId, token);

            _logger.LogInformation("Account deleted");

            return Ok();
        }
    }
}