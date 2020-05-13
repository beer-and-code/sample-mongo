using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Sample.Api.Models.Entities;

namespace Sample.Api.InfraMongo
{
    public class MongoBaseRepository<TParent, TChild> : IMongoBaseRepository<TChild>
        where TParent : IEntity
        where TChild : TParent
    {
        protected readonly IMongoClient Client;

        protected readonly IFilteredMongoCollection<TChild> FilteredCollection;

        public MongoBaseRepository(IMongoClient client, IConfiguration configuration)
        {
            Client = client;

            var collection = client
                .GetDatabase(configuration["Mongo:DatabaseName"])
                .GetCollection<TParent>(configuration["Mongo:CollectionName"]);

            FilteredCollection = collection.OfType<TChild>();
        }

        public virtual async Task AddAsync(TChild obj, CancellationToken cancellationToken)
        {
            await FilteredCollection.InsertOneAsync(obj, cancellationToken: cancellationToken);
        }

        public virtual async Task UpdateAsync(TChild obj, CancellationToken cancellationToken)
        {
            await FilteredCollection.ReplaceOneAsync(x => x.Id == obj.Id, obj, cancellationToken: cancellationToken);
        }

        public virtual async Task<TChild> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await FilteredCollection.Find(Builders<TChild>.Filter.Eq(x => x.Id, id))
                .SingleOrDefaultAsync(cancellationToken);
        }

        public virtual async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await FilteredCollection.DeleteOneAsync(x => x.Id == id, cancellationToken);
        }

        public Task<IClientSessionHandle> StartSessionAsync(CancellationToken cancellationToken)
        {
            return Client.StartSessionAsync(cancellationToken: cancellationToken);
        }

        public virtual async Task<List<TChild>> FindAsync(CancellationToken cancellationToken)
        {
            return await FilteredCollection.Find(_ => true).ToListAsync(cancellationToken);
        }
    }
}