using Jgcarmona.Qna.Domain.Abstract.Repositories.Full;
using Jgcarmona.Qna.Domain.Views;
using Jgcarmona.Qna.Infrastructure.Repositories.MongoDB.Repositories;
using MongoDB.Driver;

namespace Jgcarmona.Qna.Infrastructure.Persistence.MongoDB.Repositories.Full
{
    public class UserProfileViewRepository : MongoRepository<UserProfileView>, IUserProfileViewRepository
    {
        public UserProfileViewRepository(IMongoDatabase database) : base(database, "UserProfiles")
        {
        }
    }
}
