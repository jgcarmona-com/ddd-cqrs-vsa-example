using Jgcarmona.Qna.Domain.Entities;

namespace Jgcarmona.Qna.Domain.Events;

public class AccountCreatedEvent : EventBase
{
    public Account Account { get; }

    public AccountCreatedEvent(Account account)
    {
        Account = account ?? throw new ArgumentNullException(nameof(account));
    }
}
