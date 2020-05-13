using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Sample.Api.Controllers
{
    [Route("api/v1/accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly ILogger<AccountsController> _logger;

        // TODO Add routes
        public AccountsController(ILogger<AccountsController> logger)
        {
            _logger = logger;
        }

       

        ///// <summary>
        /////     Deposit to an account
        ///// </summary>
        //[HttpPatch("deposit")]
        //public async Task<IActionResult> Deposit(DepositRequest Request)
        //{
        //    return await PatchAsync(Request);
        //}

        ///// <summary>
        /////     Get an account balance
        ///// </summary>
        //[HttpGet("{accountId}", Name = "GetAccount")]
        //public async Task<IActionResult> Get(Guid accountId)
        //{
        //    var request = new GetAccountDetailsQuery(accountId);

        //    return await GetAsync(request);
        //}

        ///// <summary>
        /////     Withdraw from an account
        ///// </summary>
        //[HttpPatch("withdraw")]
        //public async Task<IActionResult> Withdraw(WithdrawRequest Request)
        //{
        //    return await PatchAsync(Request);
        //}

        ///// <summary>
        /////     Close the account
        ///// </summary>
        //[HttpDelete("{accountId}")]
        //public async Task<IActionResult> Close(Guid accountId)
        //{
        //    var request = new CloseAccountRequest(accountId);

        //    return await DeleteAsync(request);
        //}
    }
}