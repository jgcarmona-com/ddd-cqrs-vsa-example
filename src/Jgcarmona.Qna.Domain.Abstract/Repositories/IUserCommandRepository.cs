using Jgcarmona.Qna.Domain.Entities;

namespace Jgcarmona.Qna.Domain.Abstract.Repositories;

public interface IUserCommandRepository : ICommandRepository<User>
{
    Task<User?> GetByUsernameAsync(string username);
}
