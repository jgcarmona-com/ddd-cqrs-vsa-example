using NUlid;

namespace Jgcarmona.Qna.Infrastructure.Repositories.MongoDB.Views
{
    public class QuestionView
    {
        public Ulid Id { get; set; }
        public string Moniker { get; set; } = string.Empty;
        public string Title { get; set; }
        public string Content { get; set; }
        public List<AnswerView> Answers { get; set; }
        public int TotalVotes { get; set; }
    }
}
