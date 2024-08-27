using Jgcarmona.Qna.Domain.Abstract.Repositories.Full;
using Jgcarmona.Qna.Domain.Views;
using Jgcarmona.Qna.Infrastructure.Repositories.MongoDB.Repositories;
using MongoDB.Driver;

namespace Jgcarmona.Qna.Infrastructure.Persistence.MongoDB.Repositories.Full
{
    public class UserViewRepository : MongoRepository<UserView>, IUserViewRepository
    {
        public UserViewRepository(IMongoDatabase database) : base(database, "users")
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
