using System;
using Sample.Api.Models.ValueObjects;

namespace Sample.Api.Models.Entities
{
    public abstract class Transaction : Entity
    {
        protected Transaction()
        {
            TransactionDate = DateTime.Now;
        }

        public Amount Amount { get; protected set; }
        public DateTime TransactionDate { get; protected set; }
        public Guid AccountId { get; protected set; }
        public string Description { get; protected set; }
    }
}