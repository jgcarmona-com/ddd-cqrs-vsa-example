
using NUlid;
using System.Text.Json.Serialization;

namespace Jgcarmona.Qna.Domain.Entities
{
    public class Answer : BaseEntity
    {
        public string Content { get; set; } = string.Empty;
        public Ulid AuthorId { get; set; }
        public UserProfile Author { get; set; }
        public Ulid QuestionId { get; set; }
        [JsonIgnore]
        public Question Question { get; set; }
        public bool IsAccepted { get; set; }
        public List<Vote> Votes { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();
    }
}
