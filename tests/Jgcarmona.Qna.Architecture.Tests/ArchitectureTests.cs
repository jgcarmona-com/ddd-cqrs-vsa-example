using MediatR;
using NetArchTest.Rules;

namespace Jgcarmona.Qna.Architecture.Tests
{
    public class ArchitectureTests
    {
        private const string ApiNamespace = "Jgcarmona.Qna.Api";
        private const string ApplicationNamespace = "Jgcarmona.Qna.Application";
        private const string DomainNamespace = "Jgcarmona.Qna.Domain";
        private const string InfrastructureNamespace = "Jgcarmona.Qna.Infrastructure";
        private const string InfrastructurePersistenceNamespace = "Jgcarmona.Qna.Infrastructure.Persistence";

        [Fact]
        public void Domain_Should_Not_Have_Dependencies_On_Application_Or_Infrastructure()
        {
            var result = Types
                .InAssembly(typeof(Jgcarmona.Qna.Domain.Entities.Account).Assembly)
                .ShouldNot()
                .HaveDependencyOnAny(ApplicationNamespace, InfrastructureNamespace)
                .GetResult();

            Assert.Equal("No failing types.", GetFailingTypesMessage(result));
            Assert.True(result.IsSuccessful, $"Domain should not depend on Application or Infrastructure.");
        }

        [Fact]
        public void Application_Should_Not_Have_Dependencies_On_Infrastructure()
        {
            var result = Types
                .InAssembly(typeof(Jgcarmona.Qna.Application.Accounts.Commands.SignUp.SignupCommand).Assembly)
                .ShouldNot()
                .HaveDependencyOn(InfrastructureNamespace)
                .GetResult();

            Assert.Equal("No failing types.", GetFailingTypesMessage(result));
            Assert.True(result.IsSuccessful, $"Application should not depend on Infrastructure. ");
        }

        [Fact]
        public void Infrastructure_Should_Have_Dependencies_On_Application_And_Domain()
        {
            var result = Types
                .InAssembly(typeof(Jgcarmona.Qna.Infrastructure.Persistence.Sql.ApplicationDbContext).Assembly)
                .That().HaveNameEndingWith("Repository")
                .Should()
                .HaveDependencyOnAny(ApplicationNamespace, DomainNamespace)
                .GetResult();

            Assert.Equal("No failing types.", GetFailingTypesMessage(result));
            Assert.True(result.IsSuccessful, $"Infrastructure should depend on Application and Domain.");
        }

        [Fact]
        public void Controllers_Should_Reside_In_ApiLayer()
        {
            var result = Types
                .InAssembly(typeof(Jgcarmona.Qna.Api.Controllers.AuthController).Assembly)
                .That()
                .HaveNameEndingWith("Controller")
                .Should()
                .ResideInNamespace($"{ApiNamespace}.Controllers")
                .GetResult();

            Assert.Equal("No failing types.", GetFailingTypesMessage(result));
            Assert.True(result.IsSuccessful, $"Controllers should reside in the API layer. ");
        }

        [Fact]
        public void Controllers_Should_Not_Have_Dependencies_On_Repositories()
        {
            var result = Types
                .InAssembly(typeof(Jgcarmona.Qna.Api.Controllers.AuthController).Assembly)
                .That()
                .HaveNameEndingWith("Controller")
                .ShouldNot()
                .HaveDependencyOn($"{InfrastructureNamespace}.Repositories")
                .GetResult();

            Assert.Equal("No failing types.", GetFailingTypesMessage(result));
            Assert.True(result.IsSuccessful, $"Controllers should not depend on Repositories. ");
        }

        [Fact]
        public void Repositories_Should_Reside_In_Infrastructure_PersistenceLayer()
        {
            var result = Types
                .InAssembly(typeof(Jgcarmona.Qna.Infrastructure.Persistence.Sql.ApplicationDbContext).Assembly)
                .That().HaveNameEndingWith("Repository")
                .Should()
                .ResideInNamespace(InfrastructurePersistenceNamespace)
                .GetResult();

            Assert.Equal("No failing types.", GetFailingTypesMessage(result));
            Assert.True(result.IsSuccessful, $"Repositories should reside in Infrastructure.Persistence layer.");
        }

        [Fact(Skip = "Not implemented yet")]
        public void Entities_And_ValueObjects_Should_Be_Sealed()
        {
            var result = Types
                .InAssembly(typeof(Jgcarmona.Qna.Domain.Entities.Account).Assembly)
                .That().HaveNameEndingWith("Entity")
                .Or().HaveNameEndingWith("ValueObject")
                .Should()
                .BeSealed()
                .GetResult();

            Assert.Equal("No failing types.", GetFailingTypesMessage(result));
            Assert.True(result.IsSuccessful, $"Entities and Value Objects must be sealed.");
        }
        private string GetFailingTypesMessage(TestResult result)
        {
            return result.IsSuccessful
                ? "No failing types."
                : $" Failing types: {GetFailingTypesMessage(result)}";
        }
    }
}
