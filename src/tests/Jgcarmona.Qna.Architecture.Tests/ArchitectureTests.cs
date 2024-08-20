using NetArchTest.Rules;
using Xunit;

namespace Jgcarmona.Qna.Architecture.Tests
{
    public class ArchitectureTests
    {
        private const string DomainNamespace = "Jgcarmona.Qna.Domain";
        private const string ApplicationNamespace = "Jgcarmona.Qna.Application";
        private const string InfrastructureNamespace = "Jgcarmona.Qna.Infrastructure";
        private const string ApiWebNamespace = "Jgcarmona.Qna.Api.Web";
        private const string ApiMinimalNamespace = "Jgcarmona.Qna.Api.Minimal";

        [Fact]
        public void Domain_Should_Not_Have_Dependencies_On_Application_Or_Infrastructure()
        {
            var result = Types
                .InAssembly(typeof(Jgcarmona.Qna.Domain.Entities.User).Assembly)
                .ShouldNot()
                .HaveDependencyOnAny(ApplicationNamespace, InfrastructureNamespace)
                .GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void Application_Should_Not_Have_Dependencies_On_Infrastructure()
        {
            var result = Types
                .InAssembly(typeof(Jgcarmona.Qna.Application.Features.Users.UserService).Assembly)
                .ShouldNot()
                .HaveDependencyOn(InfrastructureNamespace)
                .GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void Infrastructure_Should_Have_Dependencies_On_Domain_And_Application()
        {
            var result = Types
                .InAssembly(typeof(Jgcarmona.Qna.Infrastructure.Persistence.ApplicationDbContext).Assembly)
                .Should()
                .HaveDependencyOnAny(DomainNamespace, ApplicationNamespace)
                .GetResult();

            Assert.True(result.IsSuccessful);
        }

        // TODO: review this architectural pattern as well as its tests
        // // [Fact]
        // // public void Controllers_Should_Not_Depend_On_Repositories_Directly()
        // // {
        // //     var result = Types
        // //         .InAssembly(typeof(Jgcarmona.Qna.Api.Web.Controllers.AuthController).Assembly)
        // //         .That()
        // //         .HaveNameEndingWith("Controller")
        // //         .ShouldNot()
        // //         .HaveDependencyOn($"{InfrastructureNamespace}.Repositories")
        // //         .GetResult();

        // //     Assert.True(result.IsSuccessful);
        // // }
    }
}
