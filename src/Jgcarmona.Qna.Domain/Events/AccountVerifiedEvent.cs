using NUlid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
