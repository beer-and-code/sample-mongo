using Sample.Api.Domain.Core;
using Sample.Api.Domain.ValueObjects;
using System;

namespace Sample.Api.Domain.Entities
{
    public class Transaction : Entity
    {
        public Transaction()
        {
            TransactionDate = DateTime.Now;
        }

        public Amount Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public Guid AccountId { get; set; }
        public string Description { get; set; }
    }
}