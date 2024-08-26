using Jgcarmona.Qna.Infrastructure.Repositories.MongoDB.Repositories;
using Jgcarmona.Qna.Infrastructure.Repositories.MongoDB.Views;
using MongoDB.Driver;

namespace Jgcarmona.Qna.Infrastructure.Persistence.MongoDB.Repositories.Full
{
    public class QuestionRepository : MongoRepository<QuestionView>, IQuestionRepository
    {
        public QuestionRepository(IMongoDatabase database) : base(database, "questions")
        {
        }

        public async Task<QuestionView?> GetByMonikerAsync(string moniker)
        {
            var filter = Builders<QuestionView>.Filter.Eq(q => q.Moniker, moniker);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<QuestionView?> GetByTitleAsync(string title)
        {
            var filter = Builders<QuestionView>.Filter.Eq(q => q.Title, title);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }
    }
}
