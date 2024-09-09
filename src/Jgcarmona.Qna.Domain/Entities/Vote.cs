using NUlid;
using System.Text.Json.Serialization;

namespace Jgcarmona.Qna.Domain.Entities
{
    public class Vote : BaseEntity
    {
        public bool IsUpvote { get; set; }

        public Ulid AuthorId { get; set; }
        [JsonIgnore]
        public UserProfile Author { get; set; }

        public Ulid? QuestionId { get; set; }
        [JsonIgnore]
        public Question? Question { get; set; }

        public Ulid? AnswerId { get; set; }
        [JsonIgnore]
        public Answer? Answer { get; set; }
    }
}
