using Jgcarmona.Qna.Domain.Entities;
using Jgcarmona.Qna.Domain.Repositories.Command;
using Microsoft.EntityFrameworkCore;

namespace Jgcarmona.Qna.Infrastructure.Persistence.Sql.Repositories
{
    public class AccountCommandRepository : CommandRepository<Account>, IAccountCommandRepository
    {
        public AccountCommandRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<Account?> GetByEmailAsync(string name)
        {
            return _context.Accounts
                .Include(a => a.Profiles)
                .FirstOrDefaultAsync(u => u.Email == name);
        }
    }
}
