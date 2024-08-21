using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;
using Jgcarmona.Qna.Application.Features.Auth;

namespace Jgcarmona.Qna.Api.Extensions;

public static class MediatRServiceCollectionExtensions
{
    public static IServiceCollection AddMediatRConfiguration(this IServiceCollection services)
    {
        // Configure MediatR using the assembly that contains the handlers
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(AuthService).Assembly);
        });
        return services;
    }
}