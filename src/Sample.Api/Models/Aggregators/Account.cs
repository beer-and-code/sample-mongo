using System;
using Sample.Api.Models.Collections;
using Sample.Api.Models.Entities;
using Sample.Api.Shared;

namespace Sample.Api.Models.Aggregators
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
                throw new DomainException($"A conta {Id} não possui fundos suficientes para retirar {debit.Amount}.");

            Transactions.Add(debit);
        }

        public void CanBeClosed()
        {
            if (Transactions.GetCurrentBalance() > 0)
                throw new DomainException($"A conta {Id} não pode ser fechada porque possui fundos.");
        }

        public decimal GetCurrentBalance()
        {
            return Transactions.GetCurrentBalance();
        }
    }
}