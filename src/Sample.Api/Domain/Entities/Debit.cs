using System;

namespace Sample.Api.Domain.Entities
{
    public sealed class Debit : Transaction
    {
        public Debit()
        {

        }
        public Debit(Guid accountId, decimal amount)
        {
            AccountId = accountId;
            Amount = amount;
            TransactionDate = DateTime.Now;
            Description = nameof(Debit);
        }

        public Debit(Guid transactionId, Guid accountId, decimal amount, DateTime transactionDate)
        {
            Id = transactionId;
            AccountId = accountId;
            Amount = amount;
            TransactionDate = transactionDate;
            Description = nameof(Debit);
        }
    }
}