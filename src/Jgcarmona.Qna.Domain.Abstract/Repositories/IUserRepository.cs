using Jgcarmona.Qna.Domain.Entities;

namespace Jgcarmona.Qna.Domain.Abstract.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByUsernameAsync(string username);
}
