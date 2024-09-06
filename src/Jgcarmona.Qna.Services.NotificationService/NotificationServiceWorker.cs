using Jgcarmona.Qna.Common.Configuration;
using Jgcarmona.Qna.Domain.Services;
using Jgcarmona.Qna.Services.Common;
using Microsoft.Extensions.Options;

namespace Jgcarmona.Qna.Services.NotificationService;

public class NotificationServiceWorker : EventProcessingBackgroundService<NotificationServiceWorker>
{
    public NotificationServiceWorker(
        IEventListener messagingListener,
        IServiceProvider serviceProvider,
        ILogger<NotificationServiceWorker> logger,
        IOptions<FeatureFlags> featureFlags)
            : base(
                messagingListener,
                serviceProvider,
                logger,
                featureFlags)
    {
    }
}