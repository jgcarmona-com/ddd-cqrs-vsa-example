using NUlid;
using System.Linq.Expressions;

namespace Jgcarmona.Qna.Domain.Abstract.Repositories.Query
{
    public interface IQueryRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Ulid id);
    }
}
