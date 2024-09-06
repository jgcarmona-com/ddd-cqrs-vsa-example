using Jgcarmona.Qna.Common.Configuration;
using Jgcarmona.Qna.Domain.Services;
using Jgcarmona.Qna.Services.Common;
using Microsoft.Extensions.Options;

namespace Jgcarmona.Qna.Services.SyncService;

public class SyncServiceWorker : EventProcessingBackgroundService<SyncServiceWorker>
{
    public SyncServiceWorker(
        IEventListener messagingListener, 
        IServiceProvider serviceProvider,
        ILogger<SyncServiceWorker> logger, 
        IOptions<FeatureFlags> featureFlags)
            : base(
                messagingListener, 
                serviceProvider, 
                logger, 
                featureFlags)
    {
    }
}
