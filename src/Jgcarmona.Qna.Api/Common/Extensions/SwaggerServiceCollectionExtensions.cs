using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace Jgcarmona.Qna.Api.Common.Extensions;

public static class SwaggerServiceCollectionExtensions
{
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
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

            options.DocInclusionPredicate((_, api) => true);

            // Group endpoints by controller or GroupName
            options.TagActionsBy(api => new[] { api.GroupName ?? api.ActionDescriptor.RouteValues["controller"] });


            // OAuth2 Definition
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
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
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
