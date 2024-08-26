using Jgcarmona.Qna.Common.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Jgcarmona.Qna.Infrastructure.Persistence.MongoDB.Extensions
{
    public static class MongoDbServiceCollectionExtensions
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoSettings = new MongoDbSettings();
            configuration.GetSection("MongoDbSettings").Bind(mongoSettings);
            MongoClientSettings settings = MongoClientSettings.FromConnectionString(mongoSettings.ConnectionString);

            var mongoClient = new MongoClient(settings);
            var mongoDatabase = mongoClient.GetDatabase(mongoSettings.DatabaseName);

            services.AddSingleton(mongoDatabase);
            services.AddSingleton<MongoDbInitializer>();

            return services;
        }

        public static async Task InitializeMongoDbAsync(this IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var initializer = scope.ServiceProvider.GetRequiredService<MongoDbInitializer>();
            await initializer.EnsureDatabaseIsReady();
        }
    }
}
