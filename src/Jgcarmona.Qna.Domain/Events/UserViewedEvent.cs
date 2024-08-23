using NUlid;

namespace Jgcarmona.Qna.Domain.Events
{
    public class UserViewedEvent : EventBase
    {
        public Ulid UserId { get; }
        public string Username { get; }
        public string Role { get; }

        public UserViewedEvent(Ulid userId, string username, string role)
        {
            UserId = userId;
            Username = username;
            Role = role;
        }
    }
}
