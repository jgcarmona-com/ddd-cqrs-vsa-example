using Jgcarmona.Qna.Domain.Repositories.Query;
using Jgcarmona.Qna.Domain.Views;
using MongoDB.Driver;

namespace Jgcarmona.Qna.Infrastructure.Persistence.MongoDB.Repositories.Queries
{
    public class QuestionViewQueryRepository : MongoQueryRepository<QuestionView>, IQuestionViewQueryRepository
    {
        public QuestionViewQueryRepository(IMongoDatabase database) : base(database, "Questions")
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
