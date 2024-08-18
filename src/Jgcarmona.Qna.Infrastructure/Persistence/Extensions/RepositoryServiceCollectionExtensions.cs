using Microsoft.Extensions.DependencyInjection;
using Jgcarmona.Qna.Infrastructure.Persistence.Repositories;

namespace Jgcarmona.Qna.Infrastructure.Persistence.Extensions
{
    public static class RepositoryServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAnswerRepository, AnswerRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IVoteRepository, VoteRepository>();
            // Add other repositories as needed

            return services;
        }
    }
}
