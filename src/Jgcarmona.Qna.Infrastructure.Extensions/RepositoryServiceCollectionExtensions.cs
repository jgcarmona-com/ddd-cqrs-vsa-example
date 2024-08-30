using Jgcarmona.Qna.Domain.Abstract.Repositories.Command;
using Jgcarmona.Qna.Domain.Abstract.Repositories.Full;
using Jgcarmona.Qna.Domain.Abstract.Repositories.Queries;
using Jgcarmona.Qna.Domain.Abstract.Repositories.Query;
using Jgcarmona.Qna.Domain.Entities;
using Jgcarmona.Qna.Domain.Views;
using Jgcarmona.Qna.Infrastructure.Persistence.MongoDB.Repositories.Full;
using Jgcarmona.Qna.Infrastructure.Persistence.MongoDB.Repositories.Queries;
using Jgcarmona.Qna.Infrastructure.Persistence.Sql.Repositories;
using Jgcarmona.Qna.Infrastructure.Repositories.MongoDB.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Jgcarmona.Qna.Infrastructure.Extensions
{
    public static class RepositoryServiceCollectionExtensions
    {
        public static IServiceCollection AddCommandRepositories(this IServiceCollection services)
        {
            // Command repositories based on SQL
            services.AddScoped<IAccountCommandRepository, AccountCommandRepository>();
            services.AddScoped<ICommandRepository<UserProfile>, UserProfileCommandRepository>();
            services.AddScoped<ICommandRepository<Question>, QuestionCommandRepository>();
            services.AddScoped<ICommandRepository<Answer>, AnswerCommandRepository>();
            services.AddScoped<ICommandRepository<Vote>, VoteCommandRepository>();
            services.AddScoped<ICommandRepository<Comment>, CommentCommandRepository>();

            return services;
        }

        public static IServiceCollection AddQueryRepositories(this IServiceCollection services)
        {
            // Query repositories based on MongoDB (Account and Question views,containing domain aggregates)
            services.AddScoped<IAccountViewQueryRepository, AccountViewQueryRepository>();
            services.AddScoped<IUserProfileViewQueryRepository, UserProfileViewQueryRepository>();
            services.AddScoped<IQuestionViewQueryRepository, QuestionViewQueryRepository>();

            return services;
        }

        public static IServiceCollection AddSyncRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAccountViewRepository, AccountViewRepository>();
            services.AddScoped<IUserProfileViewRepository, UserProfileViewRepository>();
            services.AddScoped<IQuestionViewRepository, QuestionViewRepository>();
            return services;
        }
    }
}
