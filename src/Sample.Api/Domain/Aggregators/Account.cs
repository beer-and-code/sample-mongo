using Sample.Api.Domain.Collections;
using Sample.Api.Domain.Core;
using Sample.Api.Domain.Entities;
using Sample.Api.Shared;
using System;

namespace Sample.Api.Domain.Aggregators
{
    public sealed class Account : AggregateRoot
    {
        public Account()
        {
            Transactions = new TransactionCollection();
        }

        public Guid CustomerId { get; set; }

        public TransactionCollection Transactions { get; set; }

        public void Open(Guid customerId, Credit credit)
        {
            CustomerId = customerId;
            Transactions.Add(credit);
        }

        public void Deposit(Credit credit)
        {
            Transactions.Add(credit);
        }

        public void Withdraw(Debit debit)
        {
            if (Transactions.GetCurrentBalance() < debit.Amount)
                throw new DomainException($"The account {Id} does not have enough funds to withdraw {debit.Amount}.");

            Transactions.Add(debit);
        }

        public void CanBeClosed()
        {
            if (Transactions.GetCurrentBalance() > 0)
                throw new DomainException($"The account {Id} can not be closed because it has funds.");
        }

        public decimal CurrentBalance => Transactions.GetCurrentBalance();
    }
}