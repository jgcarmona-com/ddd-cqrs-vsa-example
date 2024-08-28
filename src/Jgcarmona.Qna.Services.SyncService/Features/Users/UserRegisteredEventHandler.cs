using Jgcarmona.Qna.Domain.Abstract.Repositories.Full;
using Jgcarmona.Qna.Domain.Events;
using Jgcarmona.Qna.Domain.Views;

namespace Jgcarmona.Qna.Services.SyncService.Features.Users
{
    public class UserRegisteredEventHandler : IEventHandler<UserRegisteredEvent>
    {
        private readonly IUserViewRepository _userRepository;
        private readonly ILogger<UserRegisteredEventHandler> _logger;

        public UserRegisteredEventHandler(
            IUserViewRepository userRepository, 
            ILogger<UserRegisteredEventHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task Handle(UserRegisteredEvent domainEvent)
        {
            var userView = new UserView
            {
                EntityId = domainEvent.User.Id.ToString(),
                Username = domainEvent.User.Username,
                Role = domainEvent.User.Role,
                RegisteredAt = domainEvent.User.CreatedAt
            };

            await _userRepository.AddAsync(userView);
            _logger.LogInformation("Author {Username} added to MongoDB successfully.", userView.Username);
        }
    }
}
