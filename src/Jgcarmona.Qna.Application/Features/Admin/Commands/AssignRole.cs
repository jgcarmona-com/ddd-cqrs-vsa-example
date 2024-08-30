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
            var user = await _accountRepository.GetByIdAsync(Ulid.Parse(request.UserId));
            if (user == null)
            {
                throw new Exception($"Account with id {request.UserId} not found");
            }

            // Agregar el rol si no está ya presente
            user.AddRole(request.Role);

            await _accountRepository.UpdateAsync(user);
            _logger.LogInformation($"Account {user.Username} has been assigned role {request.Role}.");

            // TODO: Add event to notify user has been assigned role

            return AccountModel.FromEntity(user);
        }
    }
}
