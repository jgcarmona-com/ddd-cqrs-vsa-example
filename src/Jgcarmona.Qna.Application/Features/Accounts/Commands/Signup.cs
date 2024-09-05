using Jgcarmona.Qna.Application.Features.Accounts.Models;
using Jgcarmona.Qna.Application.Features.Admin.Models;
using Jgcarmona.Qna.Domain.Entities;
using Jgcarmona.Qna.Domain.Events;
using Jgcarmona.Qna.Domain.Repositories.Command;
using Jgcarmona.Qna.Domain.Services;
using Jgcarmona.Qna.Domain.ValueObjects;
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

        var existingUser = await _accountRepository.GetByEmailAsync(signupModel.Email);
        if (existingUser != null)
        {
            _logger.LogWarning("Email already exists: {Email}", signupModel.Email);
            throw new Exception("Email already exists.");
        }

        var account = new Account
        {
            Email = signupModel.Email,
            PasswordHash = _passwordHasher.Hash(signupModel.Password),
            Roles = ["User"],
            IsActive = true,
            Profiles =
            [
                new UserProfile
                {
                    FirstName = "",
                    LastName = "",
                    DisplayName = signupModel.Email,
                    IsPrimary = true
                }
            ]
        };

        await _accountRepository.AddAsync(account);

        // Get the correlation ID from the HTTP context
        var correlationId = _httpContextAccessor.HttpContext?.Items["CorrelationId"]?.ToString() ?? string.Empty;

        var emailVerificationToken = VerificationToken.Create();

        var accountCreatedEvent = new AccountCreatedEvent(account, emailVerificationToken)
        {
            CorrelationId = correlationId
        };

        await _eventDispatcher.DispatchAsync(accountCreatedEvent);

        return AccountModel.FromEntity(account);
    }
}

