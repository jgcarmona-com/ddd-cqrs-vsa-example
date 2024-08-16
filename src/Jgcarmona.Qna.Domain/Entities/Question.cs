using NUlid;

namespace Jgcarmona.Qna.Domain.Entities
{
    public class Question : IdentifiableEntity
    {
        public string Title { get; set; }
        public string Content { get; set; } = string.Empty;
        public List<Answer> Answers { get; set; } = new();
        public List<Vote> Votes { get; set; } = new();
        public Ulid UserId { get; set; }
        public User User { get; set; }
    }
}
