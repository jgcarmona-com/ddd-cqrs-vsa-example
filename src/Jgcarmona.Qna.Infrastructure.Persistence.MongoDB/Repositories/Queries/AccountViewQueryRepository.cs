using Jgcarmona.Qna.Domain.Abstract.Repositories.Queries;
using Jgcarmona.Qna.Domain.Views;
using MongoDB.Driver;

namespace Jgcarmona.Qna.Infrastructure.Persistence.MongoDB.Repositories.Queries
{
    public class AccountViewQueryRepository : MongoQueryRepository<AccountView>, IAccountViewQueryRepository
    {
        public AccountViewQueryRepository(IMongoDatabase database) : base(database, "Accounts")
        {
        }

        public async Task<AccountView?> GetByNameAsync(string name)
        {
            var filter = Builders<AccountView>.Filter.Eq(u => u.Name, name);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }
    }
}
