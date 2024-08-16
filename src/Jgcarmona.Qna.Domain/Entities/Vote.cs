using NUlid;

namespace Jgcarmona.Qna.Domain.Entities
{
    public class Vote: BaseEntity
    {
        public bool IsUpvote { get; set; }
        public Ulid  QuestionId { get; set; }
        public Question Question { get; set; }
        public Ulid  UserId { get; set; }
        public User User { get; set; }
    }
}
