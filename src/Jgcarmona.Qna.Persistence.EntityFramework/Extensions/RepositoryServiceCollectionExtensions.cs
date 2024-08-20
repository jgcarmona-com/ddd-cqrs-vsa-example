using Microsoft.Extensions.DependencyInjection;
using Jgcarmona.Qna.Persistence.EntityFramework.Repositories;
using Jgcarmona.Qna.Domain.Abstract.Repositories;

namespace Jgcarmona.Qna.Persistence.EntityFramework.Extensions
{
    public static class RepositoryServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAnswerRepository, AnswerRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IVoteRepository, VoteRepository>();

            return services;
        }
    }
}
