using Jgcarmona.Qna.Domain.Abstract.Repositories.Command;
using Jgcarmona.Qna.Domain.Entities;
using Jgcarmona.Qna.Infrastructure.Persistence.Sql;

namespace Jgcarmona.Qna.Infrastructure.Persistence.Sql.Repositories
{
    public class VoteCommandRepository : CommandRepository<Vote>, ICommandRepository<Vote>
    {
        public VoteCommandRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
