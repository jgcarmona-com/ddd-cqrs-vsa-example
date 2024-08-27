using Jgcarmona.Qna.Domain.Abstract.Repositories.Queries;
using Jgcarmona.Qna.Domain.Views;
using MongoDB.Driver;

namespace Jgcarmona.Qna.Infrastructure.Persistence.MongoDB.Repositories.Queries
{
    public class UserViewQueryRepository : MongoQueryRepository<UserView>, IUserViewQueryRepository
    {
        public UserViewQueryRepository(IMongoDatabase database) : base(database, "users")
        {
        }

        public async Task<UserView?> GetByUsernameAsync(string username)
        {
            var filter = Builders<UserView>.Filter.Eq(u => u.Username, username);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<UserView?> GetByMonikerAsync(string moniker)
        {
            var filter = Builders<UserView>.Filter.Eq(u => u.Moniker, moniker);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }
    }
}
