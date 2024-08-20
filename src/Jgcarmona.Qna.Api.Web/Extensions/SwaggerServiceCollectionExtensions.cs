using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Jgcarmona.Qna.Api.Web;

namespace Jgcarmona.Qna.Api.Web.Extensions;

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
                Contact = new Microsoft.OpenApi.Models.OpenApiContact
                {
                    Name = Constants.Contact["name"],
                    Url = new Uri(Constants.Contact["url"]),
                    Email = Constants.Contact["email"]
                },
                License = new Microsoft.OpenApi.Models.OpenApiLicense
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
