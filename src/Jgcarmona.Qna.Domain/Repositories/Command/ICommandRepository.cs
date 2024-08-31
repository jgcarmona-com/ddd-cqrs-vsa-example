using NUlid;

namespace Jgcarmona.Qna.Domain.Repositories.Command
{
    public interface ICommandRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Ulid id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
