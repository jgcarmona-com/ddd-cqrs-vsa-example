using Jgcarmona.Qna.Domain.Entities;

namespace Jgcarmona.Qna.Domain.Abstract.Repositories.Command;

public interface IUserCommandRepository : ICommandRepository<User>
{
    Task<User?> GetByUsernameAsync(string username);
}
