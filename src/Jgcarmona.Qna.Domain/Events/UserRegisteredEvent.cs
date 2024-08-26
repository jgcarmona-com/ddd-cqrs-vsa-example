using Jgcarmona.Qna.Domain.Entities;
using NUlid;

namespace Jgcarmona.Qna.Domain.Events
{
    public class UserRegisteredEvent : EventBase
    {
        public User User { get; }

        public UserRegisteredEvent(User user)
        {
            User = user;
        }
    }
}
