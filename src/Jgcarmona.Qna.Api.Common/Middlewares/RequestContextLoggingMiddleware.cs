using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Serilog.Context;

namespace Jgcarmona.Qna.Api.Common.Middlewares
{
    public class RequestContextLoggingMiddleware
    {
        private const string CorrelationIdHeaderName = "X-Correlation-Id";
        private readonly RequestDelegate _next;

        public RequestContextLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string correlationId = GetCorrelationId(context);
            context.Items["CorrelationId"] = correlationId;

            using (LogContext.PushProperty("CorrelationId", correlationId))
            {
                await _next.Invoke(context);
            }
        }

        private static string GetCorrelationId(HttpContext context)
        {
            context.Request.Headers.TryGetValue(CorrelationIdHeaderName, out StringValues correlationId);

            return correlationId.FirstOrDefault() ?? context.TraceIdentifier;
        }
    }
}
