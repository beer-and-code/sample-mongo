using System.Collections.Generic;
using Sample.Api.Models.Entities;
using Sample.Api.Models.ValueObjects;

namespace Sample.Api.Models.Collections
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
                switch (item)
                {
                    case Debit _:
                        totalAmount -= item.Amount;
                        break;
                    case Credit _:
                        totalAmount += item.Amount;
                        break;
                }
            });

            return totalAmount;
        }
    }
}