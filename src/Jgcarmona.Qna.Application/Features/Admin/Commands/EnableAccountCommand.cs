using Jgcarmona.Qna.Application.Features.Admin.Models;
using Jgcarmona.Qna.Domain.Events;
using Jgcarmona.Qna.Domain.Repositories.Command;
using Jgcarmona.Qna.Domain.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using NUlid;

namespace Jgcarmona.Qna.Application.Features.Admin.Commands
{
    public class EnableAccountCommand : IRequest<AccountModel>
    {
        public string AccountId { get; set; }
        public EnableAccountCommand(string accountId)
        {
            AccountId = accountId;
        }
    }

    public class EnableAccountCommandHandler : IRequestHandler<EnableAccountCommand, AccountModel>
    {
        private readonly IAccountCommandRepository _accountRepository;
        private readonly ILogger<EnableAccountCommandHandler> _logger;
        private readonly IEventDispatcher _eventDispatcher;

        public EnableAccountCommandHandler(
            IAccountCommandRepository accountRepository,
            ILogger<EnableAccountCommandHandler> logger,
            IEventDispatcher eventDispatcher)
        {
            _accountRepository = accountRepository;
            _logger = logger;
            _eventDispatcher = eventDispatcher;
        }

        public async Task<AccountModel> Handle(EnableAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetByIdAsync(Ulid.Parse(request.AccountId));
            if (account == null)
            {
                throw new Exception($"Account with id {request.AccountId} not found");
            }
            if (account.IsActive)
            {
                throw new Exception($"Account with id {request.AccountId} is already enabled");
            }

            account.IsActive = true;
            await _accountRepository.UpdateAsync(account);
            _logger.LogInformation($"Account {account.Email} has been enabled.");

            // Despachar el evento de dominio
            var accountEnabledEvent = new AccountEnabledEvent(request.AccountId);
            await _eventDispatcher.DispatchAsync(accountEnabledEvent);

            return AccountModel.FromEntity(account);
        }
    }
}
