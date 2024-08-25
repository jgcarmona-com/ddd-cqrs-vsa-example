using NUlid;

namespace Jgcarmona.Qna.Infrastructure.Repositories.MongoDB.Views
{
    public class UserView
    {
        public Ulid Id { get; set; }
        public string Moniker { get; set; } = string.Empty;
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime RegisteredAt { get; set; }
        public List<QuestionSummary> QuestionsAsked { get; set; }
        public List<AnswerSummary> AnswersGiven { get; set; }
    }

    public class QuestionSummary
    {
        public Ulid Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class AnswerSummary
    {
        public Ulid Id { get; set; }
        public string Content { get; set; }
        public int Votes { get; set; }
        public DateTime AnsweredAt { get; set; }
    }
}
