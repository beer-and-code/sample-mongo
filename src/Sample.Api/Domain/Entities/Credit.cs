using System;

namespace Sample.Api.Domain.Entities
{
    public class Credit : Transaction
    {
        public Credit()
        {

        }

        public Credit(Guid accountId, decimal amount)
        {
            AccountId = accountId;
            Amount = amount;
            TransactionDate = DateTime.Now;
            Description = nameof(Credit);
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