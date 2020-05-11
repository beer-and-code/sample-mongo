using System;

namespace Sample.Api.Models.Entities
{
    public sealed class Debit : Transaction
    {
        public Debit(Guid accountId, decimal amount)
        {
            AccountId = accountId;
            Amount = amount;
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