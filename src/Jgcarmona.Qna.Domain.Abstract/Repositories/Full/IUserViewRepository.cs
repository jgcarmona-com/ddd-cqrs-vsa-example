using Jgcarmona.Qna.Domain.Views;

namespace Jgcarmona.Qna.Domain.Abstract.Repositories.Full
{
    public interface IUserViewRepository : IRepository<UserView>
    {
        Task<UserView?> GetByUsernameAsync(string username);
        Task<UserView?> GetByMonikerAsync(string moniker);
    }
}
