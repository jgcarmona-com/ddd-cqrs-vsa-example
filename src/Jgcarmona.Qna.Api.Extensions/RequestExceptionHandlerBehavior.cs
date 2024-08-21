using MediatR;
using Microsoft.Extensions.Logging;

namespace Jgcarmona.Qna.Api.Extensions
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
                // Handle specific exceptions
                _logger.LogError(ex, "Invalid Operation: {Message}", ex.Message);
                // Launch a general exception to hide the details
                throw new Exception("A configuration error occurred. Please contact support.");
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                _logger.LogError(ex, "Error during request processing: {Message}", ex.Message);
                throw new Exception("An unexpected error occurred. Please try again later.");
            }
        }
    }
}
