using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;
using Jgcarmona.Qna.Application.Features.Auth;

namespace Jgcarmona.Qna.Api.Extensions;

public static class MediatRServiceCollectionExtensions
{
    public static IServiceCollection AddMediatRConfiguration(this IServiceCollection services)
    {
        // Add MediatR using the assembly of the AuthService (as it is core to the application)
        services.AddMediatR(typeof(AuthService).Assembly);
        return services;
    }
}