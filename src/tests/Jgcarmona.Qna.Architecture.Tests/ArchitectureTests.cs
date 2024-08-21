using NetArchTest.Rules;
using Xunit;

namespace Jgcarmona.Qna.Architecture.Tests
{
    public class ArchitectureTests
    {
        private const string ApiWebNamespace = "Jgcarmona.Qna.Api.Web";
        private const string ApiMinimalNamespace = "Jgcarmona.Qna.Api.Minimal";
        private const string ApplicationNamespace = "Jgcarmona.Qna.Application";
        private const string DomainNamespace = "Jgcarmona.Qna.Domain";
        private const string PersistenceNamespace = "Jgcarmona.Qna.Persistence.EntityFramework";


        // // [Fact]
        // // public void Api_Should_Have_Dependencies_On_Persistence_ForDbInitialization()
        // // {
        // //     var result = Types
        // //         .InAssembly(typeof(Jgcarmona.Qna.Api.Web.Program).Assembly)
        // //         .Should()
        // //         .HaveDependencyOn("Jgcarmona.Qna.Persistence.EntityFramework")
        // //         .GetResult();

        // //     Assert.True(result.IsSuccessful);
        // // }

        [Fact]
        public void PrintAllTypesInApiWebAssembly()
        {
            var types = Types.InAssembly(typeof(Jgcarmona.Qna.Api.Web.Program).Assembly).GetTypes();

            foreach (var type in types)
            {
                Console.WriteLine($"Namespace: {type.Namespace}, Name: {type.Name}");
            }
        }

        [Fact]
        public void Domain_Should_Not_Have_Dependencies_On_Application_Or_Persistence()
        {
            var result = Types
                .InAssembly(typeof(Jgcarmona.Qna.Domain.Entities.User).Assembly)
                .ShouldNot()
                .HaveDependencyOnAny(ApplicationNamespace, PersistenceNamespace)
                .GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void Application_Should_Not_Have_Dependencies_On_Persistence()
        {
            var result = Types
                .InAssembly(typeof(Jgcarmona.Qna.Application.Features.Users.UserService).Assembly)
                .ShouldNot()
                .HaveDependencyOn(PersistenceNamespace)
                .GetResult();

            Assert.True(result.IsSuccessful);
        }
        [Fact]
        public void Persistence_Should_Have_Dependencies_On_Domain()
        {
            var result = Types
                .InAssembly(typeof(Jgcarmona.Qna.Persistence.EntityFramework.ApplicationDbContext).Assembly)
                .That()
                .ResideInNamespace("Jgcarmona.Qna.Persistence.EntityFramework.Repositories")
                .Should()
                .HaveDependencyOn(DomainNamespace)
                .GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void Controllers_Should_Not_Have_Dependencies_On_Persistence()
        {
            var result = Types
                .InAssembly(typeof(Jgcarmona.Qna.Api.Web.Controllers.AuthController).Assembly)
                .That()
                .HaveNameEndingWith("Controller")
                .ShouldNot()
                .HaveDependencyOn(PersistenceNamespace)
                .GetResult();

            Assert.True(result.IsSuccessful);
        }
    }
}
