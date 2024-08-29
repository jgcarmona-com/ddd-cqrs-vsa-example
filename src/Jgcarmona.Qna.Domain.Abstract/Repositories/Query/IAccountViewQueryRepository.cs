using Jgcarmona.Qna.Domain.Abstract.Repositories.Query;
using Jgcarmona.Qna.Domain.Views;

namespace Jgcarmona.Qna.Domain.Abstract.Repositories.Queries;

public interface IAccountViewQueryRepository : IQueryRepository<AccountView>
{
    Task<AccountView?> GetByNameAsync(string name);
}
