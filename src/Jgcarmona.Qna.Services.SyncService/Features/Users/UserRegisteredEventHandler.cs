using Jgcarmona.Qna.Domain.Events;
using Jgcarmona.Qna.Infrastructure.Persistence.MongoDB.Repositories.Full;
using Jgcarmona.Qna.Infrastructure.Repositories.MongoDB.Views;

namespace Jgcarmona.Qna.Services.SyncService.Features.Users
{
    public class UserRegisteredEventHandler : IEventHandler<UserRegisteredEvent>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserRegisteredEventHandler> _logger;

        public UserRegisteredEventHandler(
            IUserRepository userRepository, 
            ILogger<UserRegisteredEventHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task Handle(UserRegisteredEvent domainEvent)
        {
            var userView = new UserView
            {
                Id = domainEvent.UserId,
                Username = domainEvent.Username,
                Role = domainEvent.Role
            };

            await _userRepository.AddAsync(userView);
            _logger.LogInformation("User {Username} added to MongoDB successfully.", userView.Username);
        }
    }
}
