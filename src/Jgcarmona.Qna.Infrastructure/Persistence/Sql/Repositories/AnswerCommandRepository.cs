using Jgcarmona.Qna.Domain.Entities;
using Jgcarmona.Qna.Domain.Repositories.Command;
using Jgcarmona.Qna.Infrastructure.Persistence.Sql;

namespace Jgcarmona.Qna.Infrastructure.Persistence.Sql.Repositories
{
    public class AnswerCommandRepository : CommandRepository<Answer>, ICommandRepository<Answer>
    {
        public AnswerCommandRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
