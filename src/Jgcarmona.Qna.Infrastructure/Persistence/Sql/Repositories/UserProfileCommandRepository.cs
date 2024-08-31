using Jgcarmona.Qna.Domain.Entities;
using Jgcarmona.Qna.Domain.Repositories.Command;

namespace Jgcarmona.Qna.Infrastructure.Persistence.Sql.Repositories
{
    public class VoteCommandRepository : CommandRepository<Vote>, ICommandRepository<Vote>
    {
        public VoteCommandRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
