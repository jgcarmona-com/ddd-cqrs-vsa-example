using Jgcarmona.Qna.Domain.Abstract.Repositories.Query;
using Jgcarmona.Qna.Domain.Views;
using MongoDB.Driver;
using NUlid;
using System.Linq.Expressions;

namespace Jgcarmona.Qna.Infrastructure.Persistence.MongoDB.Repositories.Queries
{
    public class MongoQueryRepository<T> : IQueryRepository<T> where T : BaseView
    {
        protected readonly IMongoCollection<T> _collection;

        public MongoQueryRepository(IMongoDatabase database, string collectionName)
        {
            _collection = database.GetCollection<T>(collectionName);
        }

        public async Task<T?> GetByIdAsync(Ulid id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id.ToString());
            return await _collection
                .Find(filter)
                .SortByDescending(q => q.UpdatedAt)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _collection.Find(predicate).ToListAsync();
        }
    }
}
