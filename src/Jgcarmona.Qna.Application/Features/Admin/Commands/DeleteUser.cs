using Jgcarmona.Qna.Application.Features.Admin.Models;
using Jgcarmona.Qna.Domain.Abstract.Repositories.Command;
using MediatR;
using Microsoft.Extensions.Logging;
using NUlid;

namespace Jgcarmona.Qna.Application.Features.Admin.Commands
{
    public class DeleteUserCommand : IRequest<AccountModel>
    {
        public Ulid UserId { get; set; }

        public DeleteUserCommand(Ulid userId)
        {
            UserId = userId;
        }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, AccountModel> // Cambié el retorno a AccountModel
    {
        private readonly IAccountCommandRepository _accountRepository;
        private readonly ILogger<DeleteUserCommandHandler> _logger;

        public DeleteUserCommandHandler(IAccountCommandRepository accountRepository, ILogger<DeleteUserCommandHandler> logger)
        {
            _accountRepository = accountRepository;
            _logger = logger;
        }

        public async Task<AccountModel> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Deleting user with id: {request.UserId}");

            try
            {
                var user = await _accountRepository.GetByIdAsync(request.UserId);
                if (user == null)
                {
                    throw new Exception($"Account with id {request.UserId} not found");
                }

                await _accountRepository.DeleteAsync(user);

                // TODO: Add event to notify that the user has been deleted
                return AccountModel.FromEntity(user); // Devolvemos el modelo del usuario eliminado
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting user with id {request.UserId}");
                throw new Exception($"Error deleting user: {ex.Message}");
            }
        }
    }
}
