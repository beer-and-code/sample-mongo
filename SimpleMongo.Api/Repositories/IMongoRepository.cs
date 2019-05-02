using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleMongo.Api.Repositories
{
    public interface IMongoRepository<T>
    {
        Task Add(T entity);
        Task Update (T entity);
        Task Delete (string id);
        Task<T> FindById(string id);
        Task<IEnumerable<T>> FindAll();
    }
}