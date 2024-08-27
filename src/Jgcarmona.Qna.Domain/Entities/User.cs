using NUlid;

namespace Jgcarmona.Qna.Domain.Entities
{
    public class User: BaseEntity
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public List<Question> Questions { get; set; } = new();
        public List<Answer> Answers { get; set; } = new();
        public List<Vote> Votes { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();
    }
}
