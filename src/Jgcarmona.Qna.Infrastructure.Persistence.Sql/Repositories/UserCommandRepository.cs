using Jgcarmona.Qna.Domain.Abstract.Repositories;
using Jgcarmona.Qna.Domain.Entities;
using Jgcarmona.Qna.Infrastructure.Persistence.Sql;

namespace Jgcarmona.Qna.Infrastructure.Persistence.Sql.Repositories
{
    public class UserCommandRepository : CommandRepository<User>, ICommandRepository<User>
    {
        public UserCommandRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
