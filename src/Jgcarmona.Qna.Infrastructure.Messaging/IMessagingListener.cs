using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qna.Infrastructure.Messaging
{
    public interface IMessagingListener
    {
        Task StartListeningAsync(CancellationToken cancellationToken = default);
    }

}
