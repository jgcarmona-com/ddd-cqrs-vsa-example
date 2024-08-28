using Jgcarmona.Qna.Domain.Abstract.Repositories.Full;
using Jgcarmona.Qna.Domain.Views;
using Jgcarmona.Qna.Infrastructure.Repositories.MongoDB.Repositories;
using MongoDB.Driver;

namespace Jgcarmona.Qna.Infrastructure.Persistence.MongoDB.Repositories.Full
{
    public class QuestionViewRepository : MongoRepository<QuestionView>, IQuestionViewRepository
    {
        public QuestionViewRepository(IMongoDatabase database) : base(database, "Questions")
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
