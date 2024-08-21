using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Jgcarmona.Qna.Persistence.EntityFramework
{
    public class DatabaseMigrator
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DatabaseMigrator> _logger;

        public DatabaseMigrator(ApplicationDbContext context, ILogger<DatabaseMigrator> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task MigrateDatabaseAsync()
        {
            _logger.LogInformation("Starting database migration...");

            try
            {
                await _context.Database.MigrateAsync();
                _logger.LogInformation("Database migration completed.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during database migration.");
                throw;
            }
        }
    }
}
