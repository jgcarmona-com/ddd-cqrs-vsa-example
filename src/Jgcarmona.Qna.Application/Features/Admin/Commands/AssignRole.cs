using Jgcarmona.Qna.Application.Features.Admin.Models;
using Jgcarmona.Qna.Domain.Abstract.Repositories.Command;
using MediatR;
using Microsoft.Extensions.Logging;
using NUlid;

namespace Jgcarmona.Qna.Application.Features.Admin.Commands
{
    public class AssignRoleCommand : IRequest<AccountModel>
    {
        public AssignRoleCommand(string userId, string role)
        {
            UserId = userId;
            Role = role;
        }
        public string UserId { get; set; }
        public string Role { get; set; } = string.Empty;
    }

    public class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommand, AccountModel>
    {
        private readonly IAccountCommandRepository _accountRepository;
        private readonly ILogger<AssignRoleCommandHandler> _logger;

        public AssignRoleCommandHandler(IAccountCommandRepository accountRepository, ILogger<AssignRoleCommandHandler> logger)
        {
            _accountRepository = accountRepository;
            _logger = logger;
        }

        public async Task<AccountModel> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetByIdAsync(Ulid.Parse(request.UserId));
            if (account == null)
            {
                throw new Exception($"Account with id {request.UserId} not found");
            }

            account.AddRole(request.Role);

            await _accountRepository.UpdateAsync(account);
            _logger.LogInformation($"Account {account.LoginName} has been assigned role {request.Role}.");

            // TODO: Add event to notify account has been assigned role

            return AccountModel.FromEntity(account);
        }
    }
}
