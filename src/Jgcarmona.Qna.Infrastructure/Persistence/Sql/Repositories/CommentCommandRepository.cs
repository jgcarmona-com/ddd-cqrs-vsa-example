using Jgcarmona.Qna.Domain.Entities;
using Jgcarmona.Qna.Domain.Repositories.Command;

namespace Jgcarmona.Qna.Infrastructure.Persistence.Sql.Repositories
{
    public class CommentCommandRepository : CommandRepository<Comment>, ICommandRepository<Comment>
    {
        public CommentCommandRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
