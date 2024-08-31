using Jgcarmona.Qna.Domain.Repositories.Full;
using Jgcarmona.Qna.Domain.Views;
using Jgcarmona.Qna.Infrastructure.Repositories.MongoDB.Repositories;
using MongoDB.Driver;

namespace Jgcarmona.Qna.Infrastructure.Persistence.MongoDB.Repositories.Full
{
    public class AccountViewRepository : MongoRepository<AccountView>, IAccountViewRepository
    {
        public AccountViewRepository(IMongoDatabase database) : base(database, "Accounts")
        {
        }

        public async Task<AccountView?> GetByNameAsync(string name)
        {
            var filter = Builders<AccountView>.Filter.Eq(u => u.LoginName, name);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }
    }
}
