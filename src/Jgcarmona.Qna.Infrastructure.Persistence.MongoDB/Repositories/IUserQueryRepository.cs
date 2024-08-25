using Jgcarmona.Qna.Domain.Abstract.Repositories;
using Jgcarmona.Qna.Infrastructure.Repositories.MongoDB.Views;

namespace Jgcarmona.Qna.Infrastructure.Repositories.MongoDB.Repositories;

public interface IUserQueryRepository : IQueryRepository<UserView>
{
    Task<UserView?> GetByMonikerAsync(string moniker);
    Task<UserView?> GetByUsernameAsync(string username);
}