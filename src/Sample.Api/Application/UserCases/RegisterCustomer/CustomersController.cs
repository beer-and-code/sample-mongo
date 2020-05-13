using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.Api.Domain.Aggregators;
using Sample.Api.Domain.Entities;
using Sample.Api.Infrastructure;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Api.Application.UserCases.RegisterCustomer
{
    [Route("api/v1/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly IMongoBaseRepository<Account> _accountRepository;
        private readonly IMongoBaseRepository<Customer> _customerRepository;

        public CustomersController(
            ILogger<CustomersController> logger,
            IMongoBaseRepository<Account> accountRepository,
            IMongoBaseRepository<Customer> customerRepository)
        {
            _logger = logger;
            _accountRepository = accountRepository;
            _customerRepository = customerRepository;
        }

        /// <summary>
        ///     Register a new Customer
        /// </summary>
        [HttpPost]
        [SwaggerResponse(201, "The customer was created", typeof(Account))]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> Post([FromBody]RegisterNewCustomerRequest request)
        {
            var source = new CancellationTokenSource();

            var token = source.Token;

            _logger.LogInformation("Creating a new customer");

            var customer = new Customer(request.Name);

            var account = new Account();

            _logger.LogInformation("Opening a new account");

            account.Open(customer.Id, new Credit(account.Id, request.InitialAmount));

            customer.Register(account.Id);

            await _customerRepository.AddAsync(customer, token);

            await _accountRepository.AddAsync(account, token);

            _logger.LogInformation("Customer registered!");

            return Created($"api/v1/accounts/{account.Id}", account);
        }
    }
}