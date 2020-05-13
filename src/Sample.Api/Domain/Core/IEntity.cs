using System;

namespace Sample.Api.Domain.Core
{
    public interface IEntity
    {
        Guid Id { get; }
    }
}