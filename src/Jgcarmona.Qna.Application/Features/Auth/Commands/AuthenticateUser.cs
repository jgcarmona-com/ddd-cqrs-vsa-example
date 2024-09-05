using Jgcarmona.Qna.Application.Features.Auth.Models;
using Jgcarmona.Qna.Domain.Repositories.Command;
using Jgcarmona.Qna.Domain.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Jgcarmona.Qna.Application.Features.Auth.Commands
{
    public class AuthenticateUserCommand(string username, string password) : IRequest<TokenResponse>
    {
        public string Username { get; set; } = username;
        public string Password { get; set; } = password;
    }

    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, TokenResponse>
    {
        private readonly IAccountCommandRepository _accountRepository;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher _passwordHasher;

        public AuthenticateUserCommandHandler(IAccountCommandRepository accountRepository, IConfiguration configuration, IPasswordHasher passwordHasher)
        {
            _accountRepository = accountRepository;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
        }

        public async Task<TokenResponse> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT key not configured."));

            var account = await _accountRepository.GetByEmailAsync(request.Username);
            if (account == null || !_passwordHasher.Verify(account.PasswordHash, request.Password))
            {
                return null;
            }

            var selectedProfile = account.Profiles.Where(up => up.IsPrimary).FirstOrDefault();

            selectedProfile = account.Profiles.FirstOrDefault(p => p.IsPrimary);
            if (selectedProfile == null)
            {
                throw new Exception("No primary profile found.");
            }

            // Add custom claims for profile info
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, account.Email),
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim("ProfileId", selectedProfile?.Id.ToString() ?? string.Empty),
                new Claim("DisplayName", selectedProfile?.DisplayName ?? string.Empty),
            };
            // Add each role as a separate claim
            claims.AddRange(account.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(token);

            return new TokenResponse { AccessToken = accessToken };
        }
    }
}
