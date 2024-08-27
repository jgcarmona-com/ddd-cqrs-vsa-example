using Jgcarmona.Qna.Domain.Views;

namespace Jgcarmona.Qna.Domain.Abstract.Repositories.Full
{
    public interface IQuestionViewRepository : IRepository<QuestionView>
    {
        Task<QuestionView?> GetByMonikerAsync(string moniker);
        Task<QuestionView?> GetByTitleAsync(string title);
    }
}
