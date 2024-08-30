namespace Jgcarmona.Qna.Domain.Views
{
    public class UserProfileView : BaseView
    {
        public string AccountId { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public bool IsPrimary { get; set; }

        public List<QuestionSummary> QuestionsAsked { get; set; } = new();
        public List<AnswerSummary> AnswersGiven { get; set; } = new();
        public List<VoteSummary> Votes { get; set; } = new();
        public List<CommentSummary> Comments { get; set; } = new();

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

        public class VoteSummary
        {
            public string Id { get; set; }
            public string TargetId { get; set; } // ID of the question or answer being voted on
            public int Value { get; set; } // +1 for upvote, -1 for downvote
        }
    }
}
