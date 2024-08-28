using Jgcarmona.Qna.Domain.Views;
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


                await InitializeIndexes();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to connect to MongoDB.");
                throw;
            }
        }

        private async Task InitializeIndexes()
        {
            // Initialize indexes for QuestionView
            var questionCollection = _database.GetCollection<QuestionView>("Questions");
            var questionIndexKeys = Builders<QuestionView>.IndexKeys
                .Ascending(q => q.EntityId)
                .Ascending(q => q.Version);
            var questionIndexModel = new CreateIndexModel<QuestionView>(questionIndexKeys);
            await questionCollection.Indexes.CreateOneAsync(questionIndexModel);

            // Initialize indexes for UserView 
            var userCollection = _database.GetCollection<UserView>("Users");
            var userIndexKeys = Builders<UserView>.IndexKeys
                .Ascending(u => u.EntityId)
                .Ascending(u => u.Version);
            var userIndexModel = new CreateIndexModel<UserView>(userIndexKeys);
            await userCollection.Indexes.CreateOneAsync(userIndexModel);

            _logger.LogInformation("Indexes for MongoDB collections initialized successfully.");
        }
    }
}
