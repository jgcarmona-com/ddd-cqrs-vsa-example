using Jgcarmona.Qna.Domain.Abstract.Repositories.Full;
using Jgcarmona.Qna.Domain.Events;
using Jgcarmona.Qna.Domain.Views;

namespace Jgcarmona.Qna.Services.SyncService.Features.Users
{
    public class AccountCreatedEventHandler : IEventHandler<AccountCreatedEvent>
    {
        private readonly IAccountViewRepository _accountRepository;
        private readonly ILogger<AccountCreatedEventHandler> _logger;

        public AccountCreatedEventHandler(
            IAccountViewRepository accountRepository, 
            ILogger<AccountCreatedEventHandler> logger)
        {
            _accountRepository = accountRepository;
            _logger = logger;
        }

        public async Task Handle(AccountCreatedEvent domainEvent)
        {
            var account = new AccountView
            {
                EntityId = domainEvent.Id.ToString(),
                Name = domainEvent.Name,
                Roles = domainEvent.Roles,
                CreatedAt = domainEvent.CreatedAt,
                ProfileIds = domainEvent.ProfileIds
            };

            await _accountRepository.AddAsync(account);
            _logger.LogInformation("Account {Name} added to MongoDB successfully.", account.Name);
        }
    }
}
