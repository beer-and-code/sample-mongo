using System;

namespace Sample.Api.Models.Entities
{
    public class Credit : Transaction
    {
        public Credit(Guid accountId, decimal amount)
        {
            AccountId = accountId;
            Amount = amount;
        }

        public Credit(Guid transactionId, Guid accountId, decimal amount, DateTime transactionDate)
        {
            Id = transactionId;
            AccountId = accountId;
            Amount = amount;
            TransactionDate = transactionDate;
            Description = nameof(Credit);
        }
    }
}