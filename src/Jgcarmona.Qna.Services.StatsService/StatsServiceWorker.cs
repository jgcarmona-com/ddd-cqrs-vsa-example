using Jgcarmona.Qna.Common.Configuration;
using Jgcarmona.Qna.Infrastructure.Messaging;
using Jgcarmona.Qna.Services.Common;
using Microsoft.Extensions.Options;

namespace Jgcarmona.Qna.Services.StatsService;

public class StatsServiceWorker : EventProcessingBackgroundService<StatsServiceWorker>
{
    public StatsServiceWorker(
        IEventListener messagingListener,
        IServiceProvider serviceProvider,
        ILogger<StatsServiceWorker> logger,
        IOptions<FeatureFlags> featureFlags)
            : base(
                messagingListener,
                serviceProvider,
                logger,
                featureFlags)
    {
    }
}
