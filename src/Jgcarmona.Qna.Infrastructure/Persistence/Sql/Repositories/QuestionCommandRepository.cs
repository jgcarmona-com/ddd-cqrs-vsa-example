using Jgcarmona.Qna.Domain.Entities;
using Jgcarmona.Qna.Domain.Repositories.Command;
using Jgcarmona.Qna.Infrastructure.Persistence.Sql;

namespace Jgcarmona.Qna.Infrastructure.Persistence.Sql.Repositories
{
    public class QuestionCommandRepository : CommandRepository<Question>, ICommandRepository<Question>
    {
        public QuestionCommandRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
