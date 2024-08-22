using NUlid;

namespace Jgcarmona.Qna.Domain.Events
{
    public class UserRegisteredEvent : EventBase
    {
        public Ulid UserId { get; }
        public string Username { get; }
        public string Role { get; }

        public UserRegisteredEvent(Ulid userId, string username, string role)
        {
            UserId = userId;
            Username = username;
            Role = role;
        }
    }
}
