using Jgcarmona.Qna.Domain.Abstract.Repositories;
using Jgcarmona.Qna.Domain.Entities;

namespace Jgcarmona.Qna.Persistence.EntityFramework.Repositories
{
    public class VoteRepository : Repository<Vote>, IVoteRepository
    {
        public VoteRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Implement additional methods specific to User if needed
    }
}
