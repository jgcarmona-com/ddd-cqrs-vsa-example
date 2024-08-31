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
            public string Moniker { get; set; }
            public string Title { get; set; }
            public DateTime CreatedAt { get; set; }
            public int TotalVotes { get; set; }
            public int AnswerCount { get; set; }
            public bool IsAnswered { get; set; }
        }

        public class AnswerSummary
        {
            public string Id { get; set; }
            public string QuestionId { get; set; }
            public string QuestionTitle { get; set; }
            public string ContentSnippet { get; set; }
            public int Votes { get; set; }
            public DateTime AnsweredAt { get; set; }
        }

        public class CommentSummary
        {
            public string Id { get; set; }
            public string Content { get; set; }
            public string Author { get; set; }
            public DateTime PostedAt { get; set; }
            public string TargetId { get; set; } // ID of the question or answer being commented on
            public string TargetType { get; set; } // "Question" or "Answer"
        }

        public class VoteSummary
        {
            public string Id { get; set; }
            public string TargetId { get; set; } // ID of the question or answer being voted on
            public string TargetType { get; set; } // "Question" or "Answer"
            public string TargetTitle { get; set; }
            public int Value { get; set; } // +1 for upvote, -1 for downvote
        }

        public UserProfileView CreateNewVersion()
        {
            return new UserProfileView
            {
                EntityId = this.EntityId,
                AccountId = this.AccountId,
                FirstName = this.FirstName,
                LastName = this.LastName,
                DisplayName = this.DisplayName,
                DateOfBirth = this.DateOfBirth,
                ProfilePictureUrl = this.ProfilePictureUrl,
                PhoneNumber = this.PhoneNumber,
                Gender = this.Gender,
                IsPrimary = this.IsPrimary,
                QuestionsAsked = new List<QuestionSummary>(this.QuestionsAsked),
                AnswersGiven = new List<AnswerSummary>(this.AnswersGiven),
                Votes = new List<VoteSummary>(this.Votes),
                Comments = new List<CommentSummary>(this.Comments),
                CreatedAt = this.CreatedAt,
                UpdatedAt = DateTime.UtcNow,
                Version = this.Version + 1
            };
        }
    }
}
