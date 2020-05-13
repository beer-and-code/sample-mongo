using Sample.Api.Domain.Core;
using System;
using System.Collections.Generic;

namespace Sample.Api.Domain.Aggregators
{
    public sealed class Customer : AggregateRoot
    {
        public Customer()
        {
        }

        public Customer(string name)
        {
            Name = name;
            Accounts = new List<Guid>();
        }

        public Customer(Guid id, string name) : this(name)
        {
            Id = id;
        }

        public string Name { get; set; }

        public List<Guid> Accounts { get; set; }

        public void Register(Guid accountId)
        {
            Accounts.Add(accountId);
        }
    }
}