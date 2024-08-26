using Jgcarmona.Qna.Infrastructure.Repositories.MongoDB;
using Jgcarmona.Qna.Infrastructure.Repositories.MongoDB.Views;
using Jgcarmona.Qna.Infrastructure.Persistence.Sql.Repositories;
using Jgcarmona.Qna.Domain.Abstract.Repositories;
using Jgcarmona.Qna.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Jgcarmona.Qna.Infrastructure.Persistence.MongoDB.Repositories.Queries;
using Jgcarmona.Qna.Infrastructure.Repositories.MongoDB.Repositories;
using Jgcarmona.Qna.Infrastructure.Persistence.MongoDB.Repositories.Full;

namespace Jgcarmona.Qna.Infrastructure.Extensions
{
    public static class RepositoryServiceCollectionExtensions
    {
        public static IServiceCollection AddCommandRepositories(this IServiceCollection services)
        {
            // Command repositories based on SQL
            services.AddScoped<IUserCommandRepository, UserCommandRepository>();
            services.AddScoped<ICommandRepository<Question>, QuestionCommandRepository>();
            services.AddScoped<ICommandRepository<Answer>, AnswerCommandRepository>();
            services.AddScoped<ICommandRepository<Vote>, VoteCommandRepository>();

            return services;
        }

        public static IServiceCollection AddQueryRepositories(this IServiceCollection services)
        {
            // Query repositories based on MongoDB
            services.AddScoped<IQueryRepository<QuestionView>, QuestionQueryRepository>();
            services.AddScoped<IQueryRepository<UserView>, UserQueryRepository>();

            return services;
        }

        public static IServiceCollection AddSyncRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            return services;
        }
    }
}
