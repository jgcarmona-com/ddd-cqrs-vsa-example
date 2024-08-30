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

        public Task<Account?> GetByNameAsync(string name)
        {
            return _context.Accounts
                .Include(a => a.Profiles)
                .FirstOrDefaultAsync(u => u.LoginName == name);            
        }
    }
}
