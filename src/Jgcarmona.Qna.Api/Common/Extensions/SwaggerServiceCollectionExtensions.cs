using Microsoft.OpenApi.Models;

namespace Jgcarmona.Qna.Api.Common.Extensions;

public static class SwaggerServiceCollectionExtensions
{
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = Constants.Title,
                Version = "v1",
                Description = Constants.Description,
                Contact = new OpenApiContact
                {
                    Name = Constants.Contact["name"],
                    Url = new Uri(Constants.Contact["url"]),
                    Email = Constants.Contact["email"]
                },
                License = new OpenApiLicense
                {
                    Name = Constants.LicenseInfo["name"],
                    Url = new Uri(Constants.LicenseInfo["url"])
                }
            });

            // OAuth2 Definition
            c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    Password = new OpenApiOAuthFlow
                    {
                        TokenUrl = new Uri("/api/auth/token", UriKind.Relative)
                    }
                }
            });

            // Security requirement globally
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "oauth2"
                        }
                    },
                    new string[] {}
                }
            });
        });

        return services;
    }
}
