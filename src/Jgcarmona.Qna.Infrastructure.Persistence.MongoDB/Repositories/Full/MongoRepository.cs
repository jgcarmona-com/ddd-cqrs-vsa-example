using Jgcarmona.Qna.Domain.Abstract.Repositories;
using MongoDB.Driver;
using NUlid;
using System.Linq.Expressions;

namespace Jgcarmona.Qna.Infrastructure.Repositories.MongoDB.Repositories
{
    public class MongoRepository<T> : ICommandRepository<T>, IQueryRepository<T> where T : class
    {
        protected readonly IMongoCollection<T> _collection;

        public MongoRepository(IMongoDatabase database, string collectionName)
        {
            _collection = database.GetCollection<T>(collectionName);
        }

        public async Task<T?> GetByIdAsync(Ulid id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id.ToString());
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _collection.Find(predicate).ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            var filter = Builders<T>.Filter.Eq("Id", entity.GetType().GetProperty("Id")?.GetValue(entity, null).ToString());
            await _collection.ReplaceOneAsync(filter, entity);
        }

        public async Task DeleteAsync(T entity)
        {
            var filter = Builders<T>.Filter.Eq("Id", entity.GetType().GetProperty("Id")?.GetValue(entity, null).ToString());
            await _collection.DeleteOneAsync(filter);
        }
    }
}
