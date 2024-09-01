using Jgcarmona.Qna.Domain.Entities;

namespace Jgcarmona.Qna.Domain.Events
{
    public class UserProfileUpdatedEvent : EventBase
    {
        public UserProfile UserProfile { get; }

        public UserProfileUpdatedEvent(UserProfile userProfile)
        {
            UserProfile = userProfile;
        }
    }
}
