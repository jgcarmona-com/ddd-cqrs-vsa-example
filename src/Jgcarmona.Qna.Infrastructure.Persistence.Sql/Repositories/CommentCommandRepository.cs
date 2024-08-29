using Jgcarmona.Qna.Domain.Abstract.Repositories.Command;
using Jgcarmona.Qna.Domain.Entities;

namespace Jgcarmona.Qna.Infrastructure.Persistence.Sql.Repositories
{
    public class CommentCommandRepository : CommandRepository<Comment>, ICommandRepository<Comment>
    {
        public CommentCommandRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
