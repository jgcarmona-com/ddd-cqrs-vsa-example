namespace Jgcarmona.Qna.Domain.Views
{
    public class QuestionView : BaseView
    {
        public string Moniker { get; set; } = string.Empty;
        public string Title { get; set; }
        public string Content { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public List<AnswerView> Answers { get; set; } = new();
        public int TotalVotes { get; set; }
        public List<CommentView> Comments { get; set; } = new();
        public DateTime LastActivityAt { get; set; }
        public List<string> Tags { get; set; } = new();

        public class CommentView
        {
            public string Id { get; set; }
            public string Content { get; set; }
            public string AuthorId { get; set; }
            public string AuthorName { get; set; }
            public DateTime PostedAt { get; set; }
        }

        public class AnswerView : BaseView
        {
            public string Content { get; set; }
            public string AuthorId { get; set; }
            public string AuthorName { get; set; }
            public int Votes { get; set; }
            public bool IsAccepted { get; set; } // Si la respuesta ha sido marcada como aceptada
            public List<CommentView> Comments { get; set; } = new();
        }
    }

}
