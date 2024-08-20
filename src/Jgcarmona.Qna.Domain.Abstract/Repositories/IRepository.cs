using Jgcarmona.Qna.Domain.Entities;
using NUlid;
using System.Linq.Expressions;

namespace Jgcarmona.Qna.Domain.Abstract.Repositories;

public interface IRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(Ulid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
}