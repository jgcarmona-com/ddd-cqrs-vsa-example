using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Jgcarmona.Qna.Services.StatsService
{
    public class StatsService : IHostedService
    {
        private readonly ILogger<StatsService> _logger;

        public StatsService(ILogger<StatsService> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("StatsService is starting.");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("StatsService is stopping.");
            return Task.CompletedTask;
        }
    }
}
