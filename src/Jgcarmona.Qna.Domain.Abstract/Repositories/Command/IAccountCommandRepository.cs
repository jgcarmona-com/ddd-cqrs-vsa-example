using Jgcarmona.Qna.Domain.Entities;

namespace Jgcarmona.Qna.Domain.Abstract.Repositories.Command;

public interface IAccountCommandRepository : ICommandRepository<Account>
{
    Task<Account?> GetByNameAsync(string username);
}
