using NUlid;

namespace Jgcarmona.Qna.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; } = string.Empty;
        public Ulid AuthorId { get; set; }
        public UserProfile Author { get; set; }

        public Ulid? QuestionId { get; set; }
        public Question? Question { get; set; }

        public Ulid? AnswerId { get; set; }
        public Answer? Answer { get; set; }
    }

}
