using NUlid;

namespace Jgcarmona.Qna.Infrastructure.Repositories.MongoDB.Views
{
    public class AnswerView
    {
        public Ulid Id { get; set; }
        public string Content { get; set; }
        public int Votes { get; set; }
    }
}
