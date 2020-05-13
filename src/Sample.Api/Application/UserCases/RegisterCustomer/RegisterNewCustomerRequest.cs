using System.ComponentModel.DataAnnotations;

namespace Sample.Api.Application.UserCases.RegisterCustomer
{
    public class RegisterNewCustomerRequest
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        public decimal InitialAmount { get; set; }
    }
}
