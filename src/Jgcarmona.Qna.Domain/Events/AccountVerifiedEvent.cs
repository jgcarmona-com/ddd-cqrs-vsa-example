using NUlid;

namespace Jgcarmona.Qna.Domain.Events
{
    public class AccountVerifiedEvent : EventBase
    {
        public Ulid AccountId { get; }

        public AccountVerifiedEvent(Ulid accountId)
        {
            AccountId = accountId;
        }
    }
}
