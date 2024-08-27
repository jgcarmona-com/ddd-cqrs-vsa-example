using Jgcarmona.Qna.Domain.Abstract.Repositories.Query;
using Jgcarmona.Qna.Domain.Views;

namespace Jgcarmona.Qna.Domain.Abstract.Repositories.Queries
{
    public interface IQuestionViewQueryRepository : IQueryRepository<QuestionView>
    {
        Task<QuestionView?> GetByMonikerAsync(string moniker);
        Task<QuestionView?> GetByTitleAsync(string title);
    }
}
