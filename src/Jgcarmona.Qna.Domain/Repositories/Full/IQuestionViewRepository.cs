using Jgcarmona.Qna.Domain.Views;

namespace Jgcarmona.Qna.Domain.Repositories.Full
{
    public interface IQuestionViewRepository : IRepository<QuestionView>
    {
        Task<QuestionView?> GetByMonikerAsync(string moniker);
        Task<QuestionView?> GetByTitleAsync(string title);
    }
}
