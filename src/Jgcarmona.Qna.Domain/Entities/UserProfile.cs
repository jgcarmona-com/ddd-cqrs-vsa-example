using NUlid;
using System.Text.Json.Serialization;

namespace Jgcarmona.Qna.Domain.Entities
{
    public class UserProfile : BaseEntity
    {
        public Ulid AccountId { get; set; }
        public Account Account { get; set; } 
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;

        public List<Question> Questions { get; set; } = new();
        public List<Answer> Answers { get; set; } = new();
        public List<Vote> Votes { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();

        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
