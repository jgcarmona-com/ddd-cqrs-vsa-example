using Jgcarmona.Qna.Domain.Events;
using Jgcarmona.Qna.Domain.Repositories.Full;
using Microsoft.Extensions.Logging;

namespace Jgcarmona.Qna.Services.SyncService.Features.UserProfiles
{
    public class UserProfileUpdatedEventHandler : IEventHandler<UserProfileUpdatedEvent>
    {
        private readonly IUserProfileViewRepository _userProfileViewRepository;
        private readonly ILogger<UserProfileUpdatedEventHandler> _logger;

        public UserProfileUpdatedEventHandler(
            IUserProfileViewRepository userProfileViewRepository,
            ILogger<UserProfileUpdatedEventHandler> logger)
        {
            _userProfileViewRepository = userProfileViewRepository;
            _logger = logger;
        }

        public async Task Handle(UserProfileUpdatedEvent domainEvent)
        {
            var existingUserProfileView = await _userProfileViewRepository.GetByIdAsync(domainEvent.UserProfile.Id);

            if (existingUserProfileView == null)
            {
                _logger.LogWarning($"UserProfile with ID {domainEvent.UserProfile.Id} not found in MongoDB.");
                return;
            }

            var updatedUserProfileView = existingUserProfileView.CreateNewVersion();
            updatedUserProfileView.FirstName = domainEvent.UserProfile.FirstName;
            updatedUserProfileView.LastName = domainEvent.UserProfile.LastName;
            updatedUserProfileView.DisplayName = domainEvent.UserProfile.DisplayName;
            updatedUserProfileView.ProfilePictureUrl = domainEvent.UserProfile.ProfilePictureUrl;
            updatedUserProfileView.PhoneNumber = domainEvent.UserProfile.PhoneNumber;
            updatedUserProfileView.Gender = domainEvent.UserProfile.Gender;
            updatedUserProfileView.DateOfBirth = domainEvent.UserProfile.DateOfBirth;

            await _userProfileViewRepository.AddAsync(updatedUserProfileView);

            _logger.LogInformation("UserProfile '{DisplayName}' updated in MongoDB successfully.", updatedUserProfileView.DisplayName);
        }
    }
}
