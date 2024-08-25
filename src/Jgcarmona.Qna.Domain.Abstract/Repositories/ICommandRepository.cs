using Jgcarmona.Qna.Domain.Entities;
using NUlid;

namespace Jgcarmona.Qna.Domain.Abstract.Repositories
{
    public interface ICommandRepository<T> where T : BaseEntity
    {
        // To load an entity before updating it
        Task<T?> GetByIdAsync(Ulid id);  
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
