using Azure.Identity;
using Jgcarmona.Qna.Common.Configuration.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jgcarmona.Qna.Infrastructure.Extensions
{
    public static class SecretsServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomSecrets(this IServiceCollection services, IConfiguration configuration)
        {
            var featureFlags = configuration.GetSection("FeatureFlags").Get<FeatureFlags>();

            switch (featureFlags.SecretsProvider.ToLower())
            {
                case "azurekeyvault":
                    var keyVaultName = Environment.GetEnvironmentVariable("AZURE_KEYVAULT_NAME");
                    if (string.IsNullOrEmpty(keyVaultName))
                    {
                        throw new InvalidOperationException("AZURE_KEYVAULT_NAME environment variable is not set.");
                    }

                    var keyVaultUri = new Uri($"https://{keyVaultName}.vault.azure.net/");
                    var credential = new DefaultAzureCredential();

                    services.AddSingleton<IConfiguration>(config =>
                        new ConfigurationBuilder()
                            .AddAzureKeyVault(keyVaultUri, credential)
                            .Build()
                    );
                    break;

                case "localenv":
                    // This is useful for local development, where we can use the .env file
                    services.AddSingleton<IConfiguration>(config =>
                        new ConfigurationBuilder()
                            .AddEnvironmentVariables() // Cargamos las variables de entorno
                            .Build()
                    );
                    break;

                default:
                    throw new InvalidOperationException("Invalid secrets provider configured.");
            }

            return services;
        }
    }
}
