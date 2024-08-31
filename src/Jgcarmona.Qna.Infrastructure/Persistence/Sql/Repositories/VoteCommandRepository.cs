using Jgcarmona.Qna.Domain.Entities;
using Jgcarmona.Qna.Domain.Repositories.Command;

namespace Jgcarmona.Qna.Infrastructure.Persistence.Sql.Repositories
{
    public class UserProfileCommandRepository : CommandRepository<UserProfile>, ICommandRepository<UserProfile>
    {
        public UserProfileCommandRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
