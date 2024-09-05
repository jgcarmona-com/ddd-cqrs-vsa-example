using Jgcarmona.Qna.Application.Features.Accounts.Commands.SignUp;
using Jgcarmona.Qna.Application.Features.Accounts.Models;
using Jgcarmona.Qna.Domain.Entities;
using Jgcarmona.Qna.Domain.Events;
using Jgcarmona.Qna.Domain.Repositories.Command;
using Jgcarmona.Qna.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Jgcarmona.Qna.Application.Tests.Features.Accounts
{
    public class SignupCommandHandlerTests
    {
        private readonly IAccountCommandRepository _accountRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<SignupCommandHandler> _logger;
        private readonly SignupCommandHandler _handler;

        public SignupCommandHandlerTests()
        {
            _accountRepository = Substitute.For<IAccountCommandRepository>();
            _passwordHasher = Substitute.For<IPasswordHasher>();
            _eventDispatcher = Substitute.For<IEventDispatcher>();
            _httpContextAccessor = Substitute.For<IHttpContextAccessor>();
            _logger = Substitute.For<ILogger<SignupCommandHandler>>();

            _handler = new SignupCommandHandler(
                _accountRepository,
                _passwordHasher,
                _logger,
                _eventDispatcher,
                _httpContextAccessor
            );
        }

        [Fact]
        public async Task Handle_Should_Create_Account_And_Dispatch_Event_When_LoginName_Does_Not_Exist()
        {
            // Arrange
            var signupModel = new SignupModel
            {
                Email = "newuser",
                Password = "password123"
            };

            _accountRepository.GetByEmailAsync(signupModel.Email)
                .Returns((Account)null);

            _passwordHasher.Hash(signupModel.Password)
                .Returns("hashedpassword");

            _accountRepository.AddAsync(Arg.Any<Account>())
                .Returns(Task.CompletedTask);

            var command = new SignupCommand { SignupModel = signupModel };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            await _accountRepository.Received(1).GetByEmailAsync(signupModel.Email);
            await _accountRepository.Received(1).AddAsync(Arg.Is<Account>(acc => acc.Email == signupModel.Email && acc.PasswordHash == "hashedpassword"));
            await _eventDispatcher.Received(1).DispatchAsync(Arg.Any<AccountCreatedEvent>());
            Assert.NotNull(result);
            Assert.Equal(signupModel.Email, result.Email);
        }

        [Fact]
        public async Task Handle_Should_Throw_Exception_When_LoginName_Already_Exists()
        {
            // Arrange
            var signupModel = new SignupModel
            {
                Email = "existinguser",
                Password = "password123"
            };

            _accountRepository.GetByEmailAsync(signupModel.Email)
                .Returns(new Account());

            var command = new SignupCommand { SignupModel = signupModel };

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
