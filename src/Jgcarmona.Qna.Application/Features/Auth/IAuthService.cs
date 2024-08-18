using Jgcarmona.Qna.Domain.Entities;

namespace Jgcarmona.Qna.Application.Features.Auth
{
    public interface IAuthService
    {
        User? AuthenticateUser(string username, string password);
        string GenerateJwtToken(User user);
    }
}