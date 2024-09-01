using Jgcarmona.Qna.Domain.Events;

namespace Jgcarmona.Qna.Domain.Events
{
    public class AccountEnabledEvent : EventBase
    {
        public string AccountId { get; }

        public AccountEnabledEvent(string accountId)
        {
            AccountId = accountId;
        }
    }
}
