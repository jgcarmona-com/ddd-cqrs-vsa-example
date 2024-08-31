using Jgcarmona.Qna.Domain.Entities;
using Jgcarmona.Qna.Domain.Repositories.Command;

namespace Jgcarmona.Qna.Infrastructure.Persistence.Sql.Repositories
{
    public class QuestionCommandRepository : CommandRepository<Question>, ICommandRepository<Question>
    {
        public QuestionCommandRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
