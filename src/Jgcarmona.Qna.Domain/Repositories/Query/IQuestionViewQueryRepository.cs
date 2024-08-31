using Jgcarmona.Qna.Domain.Views;

namespace Jgcarmona.Qna.Domain.Repositories.Query
{
    public interface IQuestionViewQueryRepository : IQueryRepository<QuestionView>
    {
        Task<QuestionView?> GetByMonikerAsync(string moniker);
        Task<QuestionView?> GetByTitleAsync(string title);
    }
}
