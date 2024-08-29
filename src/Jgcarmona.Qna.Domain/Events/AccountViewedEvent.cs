using NUlid;

namespace Jgcarmona.Qna.Domain.Events
{
    public class AccountViewedEvent : EventBase
    {
        public Ulid UserId { get; }
        public string Username { get; }

        public AccountViewedEvent(Ulid userId, string username)
        {
            UserId = userId;
            Username = username;
        }
    }
}
