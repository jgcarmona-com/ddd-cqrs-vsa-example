using Jgcarmona.Qna.Domain.Abstract.Repositories;
using Jgcarmona.Qna.Domain.Entities;
using Jgcarmona.Qna.Infrastructure.Repositories.MongoDB.Views;

namespace Jgcarmona.Qna.Infrastructure.Repositories.MongoDB.Repositories
{
    public interface IQuestionQueryRepository : IQueryRepository<QuestionView>
    {
        Task<QuestionView?> GetByMonikerAsync(string moniker);
        Task<QuestionView?> GetByTitleAsync(string title);
    }
}
