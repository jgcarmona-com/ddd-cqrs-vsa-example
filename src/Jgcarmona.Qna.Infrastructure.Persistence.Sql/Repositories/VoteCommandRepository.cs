using Jgcarmona.Qna.Domain.Abstract.Repositories.Command;
using Jgcarmona.Qna.Domain.Entities;

namespace Jgcarmona.Qna.Infrastructure.Persistence.Sql.Repositories
{
    public class UserProfileCommandRepository : CommandRepository<UserProfile>, ICommandRepository<UserProfile>
    {
        public UserProfileCommandRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
