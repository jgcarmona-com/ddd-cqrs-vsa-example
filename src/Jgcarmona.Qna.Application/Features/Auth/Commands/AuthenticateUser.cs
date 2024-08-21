using MediatR;
using Jgcarmona.Qna.Domain.Abstract.Repositories;
using Jgcarmona.Qna.Application.Features.Auth.Models;
using Jgcarmona.Qna.Domain.Abstract.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Jgcarmona.Qna.Application.Features.Auth.Commands
{
    public class AuthenticateUserCommand(string username, string password) : IRequest<TokenResponse>
    {
        public string Username { get; set; } = username;
        public string Password { get; set; } = password;
    }

    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, TokenResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher _passwordHasher;

        public AuthenticateUserCommandHandler(IUserRepository userRepository, IConfiguration configuration, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
        }

        public async Task<TokenResponse> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {

            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT key not configured."));

            var user = await _userRepository.GetByUsernameAsync(request.Username);
            if (user == null || !_passwordHasher.Verify(user.PasswordHash, request.Password))
            {
                return null;
            }

            // Generate JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                ]),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), 
                    SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(token);

            return new TokenResponse { AccessToken = accessToken };
        }
    }
}
