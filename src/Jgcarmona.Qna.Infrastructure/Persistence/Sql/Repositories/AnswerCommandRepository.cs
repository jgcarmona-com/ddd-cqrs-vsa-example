using Jgcarmona.Qna.Domain.Entities;
using Jgcarmona.Qna.Domain.Repositories.Command;

namespace Jgcarmona.Qna.Infrastructure.Persistence.Sql.Repositories
{
    public class AnswerCommandRepository : CommandRepository<Answer>, ICommandRepository<Answer>
    {
        public AnswerCommandRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
