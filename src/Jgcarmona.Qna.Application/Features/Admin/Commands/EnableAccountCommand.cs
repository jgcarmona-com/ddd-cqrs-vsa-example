using Jgcarmona.Qna.Application.Features.Admin.Models;
using Jgcarmona.Qna.Domain.Repositories.Command;
using MediatR;
using Microsoft.Extensions.Logging;
using NUlid;

namespace Jgcarmona.Qna.Application.Features.Admin.Commands
{

    public class EnableAccountCommand : IRequest<AccountModel>
    {
        public string UserId { get; set; }
        public EnableAccountCommand(string userId)
        {
            UserId = userId;
        }
    }

    public class EnableAccountCommandHandler : IRequestHandler<EnableAccountCommand, AccountModel>
    {
        private readonly IAccountCommandRepository _accountRepository;
        private readonly ILogger<EnableAccountCommandHandler> _logger;

        public EnableAccountCommandHandler(IAccountCommandRepository accountRepository, ILogger<EnableAccountCommandHandler> logger)
        {
            _accountRepository = accountRepository;
            _logger = logger;
        }

        public async Task<AccountModel> Handle(EnableAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetByIdAsync(Ulid.Parse(request.UserId));
            if (account == null)
            {
                throw new Exception($"Account with id {request.UserId} not found");
            }
            if (!account.IsActive)
            {
                throw new Exception($"Account with id {request.UserId} is already enabled");
            }

            account.IsActive = false;
            await _accountRepository.UpdateAsync(account);
            _logger.LogInformation($"Account {account.LoginName} has been enabled.");

            // TODO: Add event to notify account has been enabled

            return AccountModel.FromEntity(account);
        }
    }
}
