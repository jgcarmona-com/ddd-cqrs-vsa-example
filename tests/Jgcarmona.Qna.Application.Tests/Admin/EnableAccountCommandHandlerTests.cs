using Jgcarmona.Qna.Application.Admin.Commands;
using Jgcarmona.Qna.Domain.Entities;
using Jgcarmona.Qna.Domain.Events;
using Jgcarmona.Qna.Domain.Repositories.Command;
using Jgcarmona.Qna.Domain.Services;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUlid;

namespace Jgcarmona.Qna.Application.Tests.Admin
{
    public class EnableAccountCommandHandlerTests
    {
        private readonly IAccountCommandRepository _accountRepository;
        private readonly ILogger<EnableAccountCommandHandler> _logger;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly EnableAccountCommandHandler _handler;

        public EnableAccountCommandHandlerTests()
        {
            _accountRepository = Substitute.For<IAccountCommandRepository>();
            _logger = Substitute.For<ILogger<EnableAccountCommandHandler>>();
            _eventDispatcher = Substitute.For<IEventDispatcher>();
            _handler = new EnableAccountCommandHandler(_accountRepository, _logger, _eventDispatcher);
        }

        [Fact]
        public async Task Handle_Should_Throw_Exception_When_Account_Not_Found()
        {
            // Arrange
            var command = new EnableAccountCommand(Ulid.NewUlid().ToString());
            _accountRepository.GetByIdAsync(Ulid.Parse(command.AccountId)).Returns((Account)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal($"Account with id {command.AccountId} not found", exception.Message);
        }

        [Fact]
        public async Task Handle_Should_Throw_Exception_When_Account_Already_Enabled()
        {
            // Arrange
            var account = new Account { IsActive = true };
            var command = new EnableAccountCommand(Ulid.NewUlid().ToString());
            _accountRepository.GetByIdAsync(Ulid.Parse(command.AccountId)).Returns(account);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal($"Account with id {command.AccountId} is already enabled", exception.Message);
        }

        [Fact]
        public async Task Handle_Should_Enable_Account_And_Dispatch_Event()
        {
            // Arrange
            var account = new Account { IsActive = false, Email = "TestUser" };
            var command = new EnableAccountCommand(Ulid.NewUlid().ToString());
            _accountRepository.GetByIdAsync(Ulid.Parse(command.AccountId)).Returns(account);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(account.IsActive);
            _accountRepository.Received(1).UpdateAsync(account);
            await _eventDispatcher.Received(1).DispatchAsync(Arg.Is<AccountEnabledEvent>(e => e.AccountId == command.AccountId));
            Assert.Equal(account.Email, result.Email);
        }
    }
}