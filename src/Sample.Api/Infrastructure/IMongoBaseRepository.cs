﻿using MongoDB.Driver;
using Sample.Api.Domain.Core;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Api.Infrastructure
{
    public interface IMongoBaseRepository<T> where T : IEntity
    {
        Task AddAsync(T obj, CancellationToken cancellationToken);
        Task<List<T>> FindAsync(CancellationToken cancellationToken);
        Task UpdateAsync(T obj, CancellationToken cancellationToken);
        Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<IClientSessionHandle> StartSessionAsync(CancellationToken cancellationToken);
    }
}