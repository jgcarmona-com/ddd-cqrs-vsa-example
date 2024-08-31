using Jgcarmona.Qna.Domain.Views;

namespace Jgcarmona.Qna.Domain.Repositories.Query;

public interface IAccountViewQueryRepository : IQueryRepository<AccountView>
{
    Task<AccountView?> GetByNameAsync(string name);
}
