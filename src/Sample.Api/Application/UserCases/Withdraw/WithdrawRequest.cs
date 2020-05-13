using System;
using System.ComponentModel.DataAnnotations;

namespace Sample.Api.Application.UserCases.Withdraw
{
    public class WithdrawRequest
    {
        [Required]
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Amount { get; set; }
    }
}
