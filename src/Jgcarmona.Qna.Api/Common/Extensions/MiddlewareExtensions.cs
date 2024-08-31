using Jgcarmona.Qna.Api.Common.Middleware;

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
