using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using SimpleMongo.Api.Models;

namespace SimpleMongo.Api.Repositories
{
    public class MongoRepository<T> : IMongoRepository<T> where T : MongoEntity
    {
        private readonly IMongoCollection<T> _collection;

        public MongoRepository(IMongoClient mongo)
        {
            _collection = mongo.GetDatabase("SampleMongo").GetCollection<T>("Samples");
        }

        public async Task Add(T entity) 
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task Update (T entity) 
        {
            await _collection.ReplaceOneAsync(x=>x.Id == entity.Id, entity);
        }

        public async Task Delete (string id) 
        {
            await _collection.DeleteOneAsync(x=>x.Id == id);
        }

        public async Task<T> FindById(string id) 
        {
            return (await _collection.FindAsync(x=> x.Id == id)).SingleOrDefault();
        }

        public async Task<IEnumerable<T>> FindAll() 
        {
            return (await _collection.FindAsync(_=> true)).ToList();
        }
    }
}