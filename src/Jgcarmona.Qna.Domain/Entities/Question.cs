using NUlid;

namespace Jgcarmona.Qna.Domain.Entities
{
    public class Question : IdentifiableEntity
    {
        public string Title { get; set; }
        public string Content { get; set; } = string.Empty;
        public List<Answer> Answers { get; set; } = new();
        public List<Vote> Votes { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();
        public List<string> Tags { get; set; } = new();
        public Ulid AuthorId { get; set; }
        public UserProfile Author { get; set; }
    }
}
