using Jgcarmona.Qna.Application.Features.Users.Models;
using Jgcarmona.Qna.Domain.Abstract.Events;
using Jgcarmona.Qna.Domain.Abstract.Repositories;
using Jgcarmona.Qna.Domain.Abstract.Services;
using Jgcarmona.Qna.Domain.Entities;
using Jgcarmona.Qna.Domain.Events;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Jgcarmona.Qna.Application.Features.Users.Commands.RegisterUser;

public class RegisterUserCommand : IRequest<UserResponse>
{
    public SignupModel SignupModel { get; set; } = null!;
}

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserResponse>
{
    private readonly IUserCommandRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IEventDispatcher _eventDispatcher;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<RegisterUserCommandHandler> _logger;

    public RegisterUserCommandHandler(
        IUserCommandRepository userRepository,
        IPasswordHasher passwordHasher,
        ILogger<RegisterUserCommandHandler> logger,
        IEventDispatcher eventDispatcher,
        IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _eventDispatcher = eventDispatcher;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public async Task<UserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var signupModel = request.SignupModel;

        var existingUser = await _userRepository.GetByUsernameAsync(signupModel.Username);
        if (existingUser != null)
        {
            _logger.LogWarning("Username already exists: {Username}", signupModel.Username);
            throw new Exception("Username already exists.");
        }

        var newUser = new User
        {
            Username = signupModel.Username,
            Role = signupModel.Role,
            PasswordHash = _passwordHasher.Hash(signupModel.Password)
        };

        await _userRepository.AddAsync(newUser);

        // Get the correlation ID from the HTTP context
        var correlationId = _httpContextAccessor.HttpContext?.Items["CorrelationId"]?.ToString() ?? string.Empty;

        // Dispatch the event
        var userRegisteredEvent = new UserRegisteredEvent(newUser.Id, newUser.Username, newUser.Role)
        {
            CorrelationId = correlationId
        };

        await _eventDispatcher.DispatchAsync(userRegisteredEvent);

        return new UserResponse
        {
            Username = newUser.Username,
            Role = newUser.Role
        };
    }
}

