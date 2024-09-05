using Jgcarmona.Qna.Domain.Events;
using Jgcarmona.Qna.Domain.Repositories.Full;

namespace Jgcarmona.Qna.Services.SyncService.Features.Accounts
{

    public class AccountVerifiedEventHandler : IEventHandler<AccountVerifiedEvent>
    {
        private readonly IAccountViewRepository _accountViewRepository;

        public AccountVerifiedEventHandler(IAccountViewRepository accountViewRepository)
        {
            _accountViewRepository = accountViewRepository;
        }

        public async Task Handle(AccountVerifiedEvent domainEvent)
        {
            var accountView = await _accountViewRepository.GetByIdAsync(domainEvent.AccountId);

            if (accountView != null)
            {
                accountView.IsVerified = true;
                accountView.VerificationTokenId = null;
                accountView.TokenCreatedOnUtc = null;
                accountView.TokenExpiresOnUtc = null;

                await _accountViewRepository.UpdateAsync(accountView);
            }
        }
    }
}
