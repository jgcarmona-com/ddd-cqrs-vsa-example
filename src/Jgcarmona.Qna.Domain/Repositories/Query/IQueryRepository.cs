using NUlid;
using System.Linq.Expressions;

namespace Jgcarmona.Qna.Domain.Repositories.Query
{
    public interface IQueryRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Ulid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    }
}
