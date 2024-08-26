namespace Jgcarmona.Qna.Domain.Abstract.Repositories;

public interface IRepository<T> : IQueryRepository<T>, ICommandRepository<T> where T : class
{
}