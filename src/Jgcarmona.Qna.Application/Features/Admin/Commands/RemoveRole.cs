using Jgcarmona.Qna.Application.Features.Admin.Models;
using Jgcarmona.Qna.Domain.Abstract.Repositories.Command;
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
            var user = await _accountRepository.GetByIdAsync(request.UserId);
            if (user == null)
            {
                throw new Exception($"Account with id {request.UserId} not found");
            }

            // Remover el rol si está presente
            user.RemoveRole(request.Role);

            await _accountRepository.UpdateAsync(user);
            _logger.LogInformation($"Account {user.Username} has been removed role {request.Role}.");

            // TODO: Add event to notify user has been removed role
            return AccountModel.FromEntity(user);
        }
    }
}
