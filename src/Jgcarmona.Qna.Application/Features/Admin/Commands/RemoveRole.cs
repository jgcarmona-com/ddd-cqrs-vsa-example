using Jgcarmona.Qna.Application.Features.Admin.Models;
using Jgcarmona.Qna.Domain.Repositories.Command;
using MediatR;
using Microsoft.Extensions.Logging;
using NUlid;

namespace Jgcarmona.Qna.Application.Features.Admin.Commands
{
    public class RemoveRoleCommand : IRequest<AccountModel>
    {
        public Ulid UserId { get; set; }
        public string Role { get; set; } = string.Empty;
    }

    public class RemoveRoleCommandHandler : IRequestHandler<RemoveRoleCommand, AccountModel>
    {
        private readonly IAccountCommandRepository _accountRepository;
        private readonly ILogger<RemoveRoleCommandHandler> _logger;

        public RemoveRoleCommandHandler(IAccountCommandRepository accountRepository, ILogger<RemoveRoleCommandHandler> logger)
        {
            _accountRepository = accountRepository;
            _logger = logger;
        }

        public async Task<AccountModel> Handle(RemoveRoleCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetByIdAsync(request.UserId);
            if (account == null)
            {
                throw new Exception($"Account with id {request.UserId} not found");
            }

            account.RemoveRole(request.Role);

            await _accountRepository.UpdateAsync(account);
            _logger.LogInformation($"Account {account.Email} has been removed role {request.Role}.");

            // TODO: Add event to notify account has been removed role
            return AccountModel.FromEntity(account);
        }
    }
}
