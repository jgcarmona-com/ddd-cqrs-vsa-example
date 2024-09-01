using NUlid;

namespace Jgcarmona.Qna.Domain.Events
{
    public class AccountRoleAssignedEvent : EventBase
    {
        public AccountRoleAssignedEvent(Ulid accountId, string role)
        {
            AccountId = accountId;
            Role = role;
        }

        public Ulid AccountId { get; set; }
        public string Role { get; set; }
    }
}
