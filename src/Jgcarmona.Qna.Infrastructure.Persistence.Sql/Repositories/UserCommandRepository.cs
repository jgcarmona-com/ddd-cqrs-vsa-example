using Jgcarmona.Qna.Domain.Abstract.Repositories.Command;
using Jgcarmona.Qna.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Jgcarmona.Qna.Infrastructure.Persistence.Sql.Repositories
{
    public class UserCommandRepository : CommandRepository<User>, IUserCommandRepository
    {
        public UserCommandRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<User?> GetByUsernameAsync(string username)
        {
            return _context.Users.FirstOrDefaultAsync(u => u.Username == username);            
        }
    }
}
