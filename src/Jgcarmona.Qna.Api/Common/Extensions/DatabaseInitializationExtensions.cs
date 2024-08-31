using Jgcarmona.Qna.Application.Initialization;
using Jgcarmona.Qna.Infrastructure.Persistence.Sql;

namespace Jgcarmona.Qna.Api.Common.Extensions
{
    public static class DatabaseInitializationExtensions
    {
        public static async Task InitializeDatabaseAsync(this IServiceProvider services, IConfiguration configuration)
        {
            using var scope = services.CreateScope();
            var migrator = scope.ServiceProvider.GetRequiredService<DatabaseMigrator>();
            var initializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();

            // Check if the database should be migrated at startup via feature flag
            if (configuration.GetValue<bool>("FeatureFlags:MigrateAtStartup"))
            {
                await migrator.MigrateDatabaseAsync();
            }

            // Seed admin user
            await initializer.SeedAdminUserAsync();
        }
    }
}
