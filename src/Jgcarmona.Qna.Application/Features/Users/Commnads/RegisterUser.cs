using Jgcarmona.Qna.Application.Features.Users.Models;
using Jgcarmona.Qna.Domain.Abstract.Repositories;
using Jgcarmona.Qna.Domain.Abstract.Services;
using Jgcarmona.Qna.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Jgcarmona.Qna.Application.Features.Users.Commands.RegisterUser;

public class RegisterUserCommand : IRequest<UserResponse>
{
    public SignupModel SignupModel { get; set; } = null!;
}

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ILogger<RegisterUserCommandHandler> _logger;

    public RegisterUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, ILogger<RegisterUserCommandHandler> logger)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
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

        return new UserResponse
        {
            Username = newUser.Username,
            Role = newUser.Role
        };
    }
}

