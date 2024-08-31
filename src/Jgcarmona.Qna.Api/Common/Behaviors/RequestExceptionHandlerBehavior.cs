using MediatR;
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
                _logger.LogError(ex, "Invalid Operation in {RequestName}: {Message} | Request: {@Request}", typeof(TRequest).Name, ex.Message, request);
                throw new InvalidOperationException("A configuration error occurred. Please contact support.");
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt in {RequestName}: {Message} | Request: {@Request}", typeof(TRequest).Name, ex.Message, request);
                throw new UnauthorizedAccessException("You are not authorized to perform this action.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled error in {RequestName}: {Message} | Request: {@Request}", typeof(TRequest).Name, ex.Message, request);
                throw new ApplicationException("An unexpected error occurred. Please try again later.");
            }
        }
    }

}
