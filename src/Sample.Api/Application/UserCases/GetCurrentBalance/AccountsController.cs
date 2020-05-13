using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.Api.Domain.Aggregators;
using Sample.Api.Infrastructure;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Api.Application.UserCases.GetCurrentBalance
{
    [Route("api/v1/accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly ILogger<AccountsController> _logger;
        private readonly IMongoBaseRepository<Account> _accountRepository;
        private readonly IMongoBaseRepository<Customer> _customerRepository;

        public AccountsController(
            ILogger<AccountsController> logger,
            IMongoBaseRepository<Account> accountRepository,
            IMongoBaseRepository<Customer> customerRepository)
        {
            _logger = logger;
            _accountRepository = accountRepository;
            _customerRepository = customerRepository;
        }

        /// <summary>
        ///     Get current account balance
        /// </summary>
        [HttpGet("{accountId}")]
        [SwaggerResponse(200, "The account was found")]
        [SwaggerResponse(404, "The account not found")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> GetCurrentBalance(Guid accountId)
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

            var customer = await _customerRepository.GetByIdAsync(account.CustomerId, token);


            return Ok(new
            {
                customer,
                account
            });
        }

    }
}