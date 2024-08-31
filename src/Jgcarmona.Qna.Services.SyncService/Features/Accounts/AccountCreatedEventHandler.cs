using Jgcarmona.Qna.Domain.Events;
using Jgcarmona.Qna.Domain.Repositories.Full;
using Jgcarmona.Qna.Domain.Views;

namespace Jgcarmona.Qna.Services.SyncService.Features.Users
{
    public class AccountCreatedEventHandler : IEventHandler<AccountCreatedEvent>
    {
        private readonly IAccountViewRepository _accountRepository;
        private readonly IUserProfileViewRepository _profileRepository;
        private readonly ILogger<AccountCreatedEventHandler> _logger;

        public AccountCreatedEventHandler(
            IAccountViewRepository accountRepository,
            IUserProfileViewRepository profileRepository,
            ILogger<AccountCreatedEventHandler> logger)
        {
            _accountRepository = accountRepository;
            _profileRepository = profileRepository;
            _logger = logger;
        }

        public async Task Handle(AccountCreatedEvent domainEvent)
        {
            var accountView = new AccountView
            {
                EntityId = domainEvent.Account.Id.ToString(),
                LoginName = domainEvent.Account.LoginName,
                Roles = domainEvent.Account.Roles,
                CreatedAt = domainEvent.Account.CreatedAt,
                Email = domainEvent.Account.Email,
                IsActive = domainEvent.Account.IsActive,
                PrimaryProfileId = domainEvent.Account.Profiles.FirstOrDefault(p => p.IsPrimary).Id.ToString(),
                ProfileIds = domainEvent.Account.Profiles.Select(p => p.Id.ToString()).ToList()
            };

            await _accountRepository.AddAsync(accountView);
            foreach (var profile in domainEvent.Account.Profiles)
            {
                var profileView = new UserProfileView
                {
                    EntityId = profile.Id.ToString(),
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    DisplayName = profile.DisplayName,
                    IsPrimary = profile.IsPrimary,
                    AccountId = domainEvent.Account.Id.ToString()
                };

                await _profileRepository.AddAsync(profileView);
            }
            _logger.LogInformation("Account {LoginName} added to MongoDB successfully.", accountView.LoginName);
        }
    }
}
