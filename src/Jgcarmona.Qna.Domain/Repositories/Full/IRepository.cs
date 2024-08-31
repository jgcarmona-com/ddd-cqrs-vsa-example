using NUlid;
using System.Linq.Expressions;

namespace Jgcarmona.Qna.Domain.Repositories.Full;

public interface IRepository<T>
{
    Task<T?> GetByIdAsync(Ulid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}