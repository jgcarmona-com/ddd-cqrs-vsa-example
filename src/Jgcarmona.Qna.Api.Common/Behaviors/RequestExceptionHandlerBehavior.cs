using MediatR;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Jgcarmona.Qna.Api.Common.Behaviors
{
    public class RequestExceptionHandlerBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<RequestExceptionHandlerBehavior<TRequest, TResponse>> _logger;

        public RequestExceptionHandlerBehavior(ILogger<RequestExceptionHandlerBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (InvalidOperationException ex)
            {
                // Log detailed information using Serilog
                Log.Error(ex, "Invalid Operation: {Message} | Request: {@Request}", ex.Message, request);

                // Hide specific details from the client
                throw new InvalidOperationException("A configuration error occurred. Please contact support.");
            }
            catch (UnauthorizedAccessException ex)
            {
                // Log unauthorized access attempts separately
                Log.Warning(ex, "Unauthorized access attempt: {Message} | Request: {@Request}", ex.Message, request);

                // Throw a more user-friendly message
                throw new UnauthorizedAccessException("You are not authorized to perform this action.");
            }
            catch (Exception ex)
            {
                // Log general exceptions
                Log.Error(ex, "Error during request processing: {Message} | Request: {@Request}", ex.Message, request);

                // Hide implementation details from the client
                throw new Exception("An unexpected error occurred. Please try again later.");
            }
        }
    }
}
