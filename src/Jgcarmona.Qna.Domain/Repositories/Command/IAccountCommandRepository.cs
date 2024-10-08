using Jgcarmona.Qna.Domain.Entities;

namespace Jgcarmona.Qna.Domain.Repositories.Command;

public interface IAccountCommandRepository : ICommandRepository<Account>
{
    Task<Account?> GetByEmailAsync(string username);
}
