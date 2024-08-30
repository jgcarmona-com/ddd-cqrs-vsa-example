using System.Security.Claims;

namespace Jgcarmona.Qna.Api.Web.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetAccountId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                   ?? throw new UnauthorizedAccessException("Account ID not found in token.");
        }

        public static string GetProfileId(this ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(c => c.Type == "ProfileId")?.Value
                   ?? throw new UnauthorizedAccessException("Profile ID not found in token.");
        }
    }
}
