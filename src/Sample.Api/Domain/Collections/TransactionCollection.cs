using Sample.Api.Domain.Entities;
using Sample.Api.Domain.ValueObjects;
using System.Collections.Generic;

namespace Sample.Api.Domain.Collections
{
    public class TransactionCollection : List<Transaction>
    {
        public TransactionCollection()
        {
        }

        public TransactionCollection(IEnumerable<Transaction> list)
        {
            foreach (var item in list) Add(item);
        }

        public Amount GetCurrentBalance()
        {
            Amount totalAmount = 0;

            ForEach(item =>
            {
                switch (item.Description)
                {
                    case nameof(Debit):
                        totalAmount -= item.Amount;
                        break;
                    case nameof(Credit):
                        totalAmount += item.Amount;
                        break;
                }
            });

            return totalAmount;
        }
    }
}