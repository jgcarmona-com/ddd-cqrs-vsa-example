using Jgcarmona.Qna.Application.Features.Admin.Commands;
using Jgcarmona.Qna.Domain.Entities;
using Jgcarmona.Qna.Domain.Events;
using Jgcarmona.Qna.Domain.Repositories.Command;
using Jgcarmona.Qna.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUlid;

namespace Jgcarmona.Qna.Application.Tests.Features.Admin
{
    public class AssignRoleCommandHandlerTests
    {
        private readonly IAccountCommandRepository _accountRepository;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AssignRoleCommandHandler> _logger;
        private readonly AssignRoleCommandHandler _handler;

        public AssignRoleCommandHandlerTests()
        {
            _accountRepository = Substitute.For<IAccountCommandRepository>();
            _eventDispatcher = Substitute.For<IEventDispatcher>();
            _httpContextAccessor = Substitute.For<IHttpContextAccessor>();
            _logger = Substitute.For<ILogger<AssignRoleCommandHandler>>();

            _handler = new AssignRoleCommandHandler(_accountRepository, _eventDispatcher, _httpContextAccessor, _logger);
        }

        [Fact]
        public async Task Handle_Should_Throw_Exception_When_Account_Not_Found()
        {
            var validUlid = Ulid.NewUlid().ToString(); // Generate a valid Ulid string
            var command = new AssignRoleCommand(validUlid, "Admin");
            _accountRepository.GetByIdAsync(Arg.Any<Ulid>()).Returns((Account)null);

            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal($"Account with id {command.AccountId} not found", exception.Message);
        }

        [Fact]
        public async Task Handle_Should_Add_Role_To_Account()
        {
            var account = new Account { Roles = new List<string> { "User" } };
            _accountRepository.GetByIdAsync(Arg.Any<Ulid>()).Returns(account);

            var command = new AssignRoleCommand(account.Id.ToString(), "Admin");

            await _handler.Handle(command, CancellationToken.None);

            Assert.Contains("Admin", account.Roles);
        }

        [Fact]
        public async Task Handle_Should_Call_UpdateAsync()
        {
            var account = new Account { Roles = new List<string> { "User" } };
            _accountRepository.GetByIdAsync(Arg.Any<Ulid>()).Returns(account);

            var command = new AssignRoleCommand(account.Id.ToString(), "Admin");

            await _handler.Handle(command, CancellationToken.None);

            await _accountRepository.Received(1).UpdateAsync(account);
        }

        [Fact]
        public async Task Handle_Should_Dispatch_DomainEvent_After_Assigning_Role()
        {
            var account = new Account { Roles = new List<string> { "User" } };
            _accountRepository.GetByIdAsync(Arg.Any<Ulid>()).Returns(account);

            var command = new AssignRoleCommand(account.Id.ToString(), "Admin");

            await _handler.Handle(command, CancellationToken.None);

            await _eventDispatcher.Received(1).DispatchAsync(Arg.Any<AccountRoleAssignedEvent>());
        }
    }
}
