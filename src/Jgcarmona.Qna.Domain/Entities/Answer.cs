
using NUlid;

namespace Jgcarmona.Qna.Domain.Entities
{
    public class Answer : BaseEntity
    {
        public string Content { get; set; } = string.Empty;
        public Ulid QuestionId { get; set; }
        public Question Question { get; set; }
        public Ulid UserId { get; set; }
        public User User { get; set; }
    }
}
