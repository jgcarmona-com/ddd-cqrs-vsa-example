using Jgcarmona.Qna.Application.Features.Admin.Models;
using Jgcarmona.Qna.Domain.Abstract.Repositories.Command;
using MediatR;
using Microsoft.Extensions.Logging;
using NUlid;

namespace Jgcarmona.Qna.Application.Features.Admin.Commands
{

    public class EnableUserCommand : IRequest<AccountModel>
    {
        public string UserId { get; set; }
        public EnableUserCommand(string userId)
        {
            UserId = userId;
        }
    }

    public class EnableUserCommandHandler : IRequestHandler<EnableUserCommand, AccountModel>
    {
        private readonly IAccountCommandRepository _accountRepository;
        private readonly ILogger<EnableUserCommandHandler> _logger;

        public EnableUserCommandHandler(IAccountCommandRepository accountRepository, ILogger<EnableUserCommandHandler> logger)
        {
            _accountRepository = accountRepository;
            _logger = logger;
        }

        public async Task<AccountModel> Handle(EnableUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _accountRepository.GetByIdAsync(Ulid.Parse(request.UserId));
            if (user == null)
            {
                throw new Exception($"Account with id {request.UserId} not found");
            }
            if (!user.IsActive)
            {
                throw new Exception($"Account with id {request.UserId} is already enabled");
            }

            user.IsActive = false;
            await _accountRepository.UpdateAsync(user);
            _logger.LogInformation($"Account {user.Username} has been enabled.");

            // TODO: Add event to notify user has been enabled

            return AccountModel.FromEntity(user);
        }
    }
}
