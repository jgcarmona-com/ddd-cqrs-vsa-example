using Jgcarmona.Qna.Api.Common.Behaviors;
using Jgcarmona.Qna.Application.Features.Auth;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Jgcarmona.Qna.Api.Common.Extensions;

public static class MediatRServiceCollectionExtensions
{
    public static IServiceCollection AddMediatRConfiguration(this IServiceCollection services)
    {
        // Configure MediatR using the assembly that contains the handlers
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(AuthService).Assembly);
        });

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestLoggingPipelineBehavior<,>));

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestExceptionHandlerBehavior<,>));

        return services;
    }
}