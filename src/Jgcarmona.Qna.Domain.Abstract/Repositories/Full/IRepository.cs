using Jgcarmona.Qna.Domain.Abstract.Repositories.Command;
using Jgcarmona.Qna.Domain.Abstract.Repositories.Query;

namespace Jgcarmona.Qna.Domain.Abstract.Repositories.Full;

public interface IRepository<T> : IQueryRepository<T>, ICommandRepository<T> where T : class
{
}