using Jgcarmona.Qna.Application.Features.Accounts.Models;
using Jgcarmona.Qna.Application.Features.Admin.Models;
using Jgcarmona.Qna.Domain.Abstract.Events;
using Jgcarmona.Qna.Domain.Abstract.Repositories.Command;
using Jgcarmona.Qna.Domain.Abstract.Services;
using Jgcarmona.Qna.Domain.Entities;
using Jgcarmona.Qna.Domain.Events;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Jgcarmona.Qna.Application.Features.Accounts.Commands.SignUp;

public class SignupCommand : IRequest<AccountModel>
{
    public SignupModel SignupModel { get; set; } = null!;
}

public class SignupCommandHandler : IRequestHandler<SignupCommand, AccountModel>
{
    private readonly IAccountCommandRepository _accountRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IEventDispatcher _eventDispatcher;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<SignupCommandHandler> _logger;

    public SignupCommandHandler(
        IAccountCommandRepository accountRepository,
        IPasswordHasher passwordHasher,
        ILogger<SignupCommandHandler> logger,
        IEventDispatcher eventDispatcher,
        IHttpContextAccessor httpContextAccessor)
    {
        _accountRepository = accountRepository;
        _passwordHasher = passwordHasher;
        _eventDispatcher = eventDispatcher;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public async Task<AccountModel> Handle(SignupCommand request, CancellationToken cancellationToken)
    {
        var signupModel = request.SignupModel;

        var existingUser = await _accountRepository.GetByNameAsync(signupModel.Username);
        if (existingUser != null)
        {
            _logger.LogWarning("Name already exists: {Name}", signupModel.Username);
            throw new Exception("Name already exists.");
        }

        var account = new Account
        {
            Username = signupModel.Username,
            PasswordHash = _passwordHasher.Hash(signupModel.Password),
            Roles = ["User"],
        };

        await _accountRepository.AddAsync(account);

        // Get the correlation ID from the HTTP context
        var correlationId = _httpContextAccessor.HttpContext?.Items["CorrelationId"]?.ToString() ?? string.Empty;
       
        var accountCreatedEvent = new AccountCreatedEvent(
            account.Username,
            account.Email,
            account.Roles,
            account.IsActive,
            account.CreatedAt,
            account.TwoFactorEnabled,
            account.Profiles.Select(p => p.Id.ToString()).ToList()
        )
        {
            CorrelationId = correlationId
        };

        await _eventDispatcher.DispatchAsync(accountCreatedEvent);

        return AccountModel.FromEntity(account);
    }
}

