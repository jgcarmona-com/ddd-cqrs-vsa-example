using Jgcarmona.Qna.Api.Common.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Jgcarmona.Qna.Api.Common.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestContextLogging(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RequestContextLoggingMiddleware>();
        }
    }
}
