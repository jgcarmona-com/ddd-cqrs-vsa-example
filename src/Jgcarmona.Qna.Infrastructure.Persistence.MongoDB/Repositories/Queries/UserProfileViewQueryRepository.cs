using Jgcarmona.Qna.Domain.Abstract.Repositories.Query;
using Jgcarmona.Qna.Domain.Views;
using MongoDB.Driver;

namespace Jgcarmona.Qna.Infrastructure.Persistence.MongoDB.Repositories.Queries
{
    public class UserProfileViewQueryRepository : MongoQueryRepository<UserProfileView>, IUserProfileViewQueryRepository
    {
        public UserProfileViewQueryRepository(IMongoDatabase database) : base(database, "UserProfiles")
        {
        }

        public async Task<UserProfileView?> GetByDisplayNameAsync(string displayName)
        {
            var filter = Builders<UserProfileView>.Filter.Eq(u => u.DisplayName, displayName);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<UserProfileView>> GetByAccountIdAsync(string accountId)
        {
            var filter = Builders<UserProfileView>.Filter.Eq(u => u.AccountId, accountId);
            return await _collection.Find(filter).ToListAsync();
        }
    }
}
