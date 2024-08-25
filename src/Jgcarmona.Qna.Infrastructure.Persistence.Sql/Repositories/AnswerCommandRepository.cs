using Jgcarmona.Qna.Domain.Abstract.Repositories;
using Jgcarmona.Qna.Domain.Entities;
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
