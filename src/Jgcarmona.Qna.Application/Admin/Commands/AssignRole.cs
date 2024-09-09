using Jgcarmona.Qna.Application.Accounts.Models;
using Jgcarmona.Qna.Domain.Events;
using Jgcarmona.Qna.Domain.Repositories.Command;
using Jgcarmona.Qna.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NUlid;

namespace Jgcarmona.Qna.Application.Admin.Commands
{
    public class AssignRoleCommand : IRequest<AccountModel>
    {
        public AssignRoleCommand(string accountId, string role)
        {
            AccountId = accountId;
            Role = role;
        }
        public string AccountId { get; set; }
        public string Role { get; set; } = string.Empty;
    }

    public class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommand, AccountModel>
    {
        private readonly IAccountCommandRepository _accountRepository;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AssignRoleCommandHandler> _logger;

        public AssignRoleCommandHandler(
            IAccountCommandRepository accountRepository,
            IEventDispatcher eventDispatcher,
            IHttpContextAccessor httpContextAccessor,
            ILogger<AssignRoleCommandHandler> logger)
        {
            _accountRepository = accountRepository;
            _eventDispatcher = eventDispatcher;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<AccountModel> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetByIdAsync(Ulid.Parse(request.AccountId));
            if (account == null)
            {
                throw new Exception($"Account with id {request.AccountId} not found");
            }

            account.AddRole(request.Role);

            await _accountRepository.UpdateAsync(account);
            _logger.LogInformation($"Account {account.Email} has been assigned role {request.Role}.");

            // Add event to notify account has been assigned role
            var correlationId = _httpContextAccessor.HttpContext?.Items["CorrelationId"]?.ToString() ?? string.Empty;
            var accountRoleAssignedEvent = new AccountRoleAssignedEvent(account.Id, request.Role)
            {
                CorrelationId = correlationId
            };

            await _eventDispatcher.DispatchAsync(accountRoleAssignedEvent);

            return AccountModel.FromEntity(account);
        }
    }
}
