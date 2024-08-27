namespace Jgcarmona.Qna.Domain.Views
{
    public class UserView : BaseView
    {
        public string Moniker { get; set; } = string.Empty;
        public string Username { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public DateTime RegisteredAt { get; set; }

        public List<QuestionSummary> QuestionsAsked { get; set; } = new();
        public List<AnswerSummary> AnswersGiven { get; set; } = new();

        public class QuestionSummary
        {
            public string Id { get; set; }
            public string Title { get; set; }
            public DateTime CreatedAt { get; set; }
            public int TotalVotes { get; set; }
            public List<CommentSummary> Comments { get; set; } = new();
        }

        public class AnswerSummary
        {
            public string Id { get; set; }
            public string Content { get; set; }
            public int Votes { get; set; }
            public DateTime AnsweredAt { get; set; }
            public List<CommentSummary> Comments { get; set; } = new();
        }

        public class CommentSummary
        {
            public string Id { get; set; }
            public string Content { get; set; }
            public string Author { get; set; }
            public DateTime PostedAt { get; set; }
        }
    }
}
