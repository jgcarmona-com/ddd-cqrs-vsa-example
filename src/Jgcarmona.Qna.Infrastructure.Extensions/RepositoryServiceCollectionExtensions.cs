using Jgcarmona.Qna.Domain.Abstract.Repositories.Command;
using Jgcarmona.Qna.Domain.Abstract.Repositories.Full;
using Jgcarmona.Qna.Domain.Abstract.Repositories.Queries;
using Jgcarmona.Qna.Domain.Entities;
using Jgcarmona.Qna.Infrastructure.Persistence.MongoDB.Repositories.Full;
using Jgcarmona.Qna.Infrastructure.Persistence.MongoDB.Repositories.Queries;
using Jgcarmona.Qna.Infrastructure.Persistence.Sql.Repositories;
using Microsoft.Extensions.DependencyInjection;

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
            // Query repositories based on MongoDB (User and Question views,containing domain aggregates)
            services.AddScoped<IQuestionViewQueryRepository, QuestionViewQueryRepository>();
            services.AddScoped<IUserViewQueryRepository, UserViewQueryRepository>();

            return services;
        }

        public static IServiceCollection AddSyncRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserViewRepository, UserViewRepository>();
            services.AddScoped<IQuestionViewRepository, QuestionViewRepository>();
            return services;
        }
    }
}
