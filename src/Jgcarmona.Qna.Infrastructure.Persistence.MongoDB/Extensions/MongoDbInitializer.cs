using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Runtime.CompilerServices;

namespace Jgcarmona.Qna.Infrastructure.Persistence.MongoDB.Extensions
{
    public class MongoDbInitializer
    {
        private readonly IMongoDatabase _database;
        private readonly ILogger<MongoDbInitializer> _logger;

        public MongoDbInitializer(IMongoDatabase database, ILogger<MongoDbInitializer> logger)
        {
            _database = database;
            _logger = logger;
        }

        public async Task EnsureDatabaseIsReady()
        {
            try
            {
                _logger.LogInformation("Checking MongoDB connection...");
                var result = await _database.RunCommandAsync((Command<BsonDocument>)"{ping:1}");
                _logger.LogInformation("Successfully connected to MongoDB.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to connect to MongoDB.");
                throw;
            }
        }
    }
}
