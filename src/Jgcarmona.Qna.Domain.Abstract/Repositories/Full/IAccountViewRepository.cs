using Jgcarmona.Qna.Domain.Views;

namespace Jgcarmona.Qna.Domain.Abstract.Repositories.Full
{
    public interface IAccountViewRepository : IRepository<AccountView>
    {
        Task<AccountView?> GetByNameAsync(string accountname);
    }
}
