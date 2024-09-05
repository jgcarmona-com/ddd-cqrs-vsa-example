using NUlid;

namespace Jgcarmona.Qna.Domain.Events
{
    public class AccountViewedEvent : EventBase
    {
        public string AccountId { get; }
        public string ViewedByProfileId { get; }

        public AccountViewedEvent(string accountId, string viewedByProfileId)
        {
            AccountId = accountId;
            ViewedByProfileId = viewedByProfileId;
        }
    }
}
