using Jgcarmona.Qna.Domain.Abstract.Repositories.Query;
using Jgcarmona.Qna.Domain.Views;

namespace Jgcarmona.Qna.Domain.Abstract.Repositories.Queries;

public interface IUserViewQueryRepository : IQueryRepository<UserView>
{
    Task<UserView?> GetByMonikerAsync(string moniker);
    Task<UserView?> GetByUsernameAsync(string username);
}
