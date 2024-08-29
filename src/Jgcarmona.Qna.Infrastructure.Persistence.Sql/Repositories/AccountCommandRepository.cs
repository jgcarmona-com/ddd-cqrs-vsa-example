using Jgcarmona.Qna.Domain.Abstract.Repositories.Command;
using Jgcarmona.Qna.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Jgcarmona.Qna.Infrastructure.Persistence.Sql.Repositories
{
    public class AccountCommandRepository : CommandRepository<Account>, IAccountCommandRepository
    {
        public AccountCommandRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<Account?> GetByNameAsync(string username)
        {
            return _context.Users.FirstOrDefaultAsync(u => u.Username == username);            
        }
    }
}
