
using NUlid;

namespace Jgcarmona.Qna.Domain.Entities
{
    public class Answer : BaseEntity
    {
        public string Content { get; set; } = string.Empty;
        public Ulid AuthorId { get; set; }
        public User Author { get; set; }
        public Ulid QuestionId { get; set; }
        public Question Question { get; set; }
        public bool IsAccepted { get; set; }
        public List<Comment> Comments { get; set; } = new();
    }
}
