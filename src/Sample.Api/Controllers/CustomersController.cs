using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Sample.Api.Controllers
{
    [Route("api/v1/customers")]
    public class CustomersController : ControllerBase
    {
        // TODO Add routes
        public CustomersController(ILogger<CustomersController> logger)
        {
            Logger = logger;
        }

        public ILogger<CustomersController> Logger { get; }

        ///// <summary>
        /////     Register a new Customer
        ///// </summary>
        //[HttpPost]
        //public async Task<IActionResult> Post(RegisterCustomerCommand command)
        //{
        //    return await PostAsync(command);
        //}
    }
}