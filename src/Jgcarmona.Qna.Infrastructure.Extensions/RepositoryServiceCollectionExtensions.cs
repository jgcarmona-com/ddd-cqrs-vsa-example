using Jgcarmona.Qna.Infrastructure.Repositories.MongoDB;
using Jgcarmona.Qna.Infrastructure.Repositories.MongoDB.Views;
using Jgcarmona.Qna.Infrastructure.Persistence.Sql.Repositories;
using Jgcarmona.Qna.Domain.Abstract.Repositories;
using Jgcarmona.Qna.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Jgcarmona.Qna.Infrastructure.Repositories.MongoDB.Repositories;

namespace Jgcarmona.Qna.Infrastructure.Extensions
{
    public static class RepositoryServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            // Repositorios de comando basados en SQL (EF Core)
            services.AddScoped<IUserCommandRepository, UserCommandRepository>();
            services.AddScoped<ICommandRepository<Question>, QuestionCommandRepository>();
            services.AddScoped<ICommandRepository<Answer>, AnswerCommandRepository>();
            services.AddScoped<ICommandRepository<Vote>, VoteCommandRepository>();

            // Repositorios de consulta basados en MongoDB
            services.AddScoped<IQueryRepository<QuestionView>, QuestionQueryRepository>();
            services.AddScoped<IQueryRepository<UserView>, UserQueryRepository>();

            return services;
        }
    }
}
