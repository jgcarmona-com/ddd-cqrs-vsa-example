using Jgcarmona.Qna.Domain.Abstract.Repositories;
using Jgcarmona.Qna.Infrastructure.Repositories.MongoDB.Views;

namespace Jgcarmona.Qna.Infrastructure.Persistence.MongoDB.Repositories.Full
{
    public interface IUserRepository : ICommandRepository<UserView>, IQueryRepository<UserView>
    {
        Task<UserView?> GetByUsernameAsync(string username);
        Task<UserView?> GetByMonikerAsync(string moniker);
    }
}
