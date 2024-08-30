using Jgcarmona.Qna.Domain.Entities;
using Jgcarmona.Qna.Domain.Abstract.Services;
using Microsoft.Extensions.Logging;
using Jgcarmona.Qna.Domain.Abstract.Events;
using Jgcarmona.Qna.Domain.Events;
using Jgcarmona.Qna.Domain.Abstract.Repositories.Command;

namespace Jgcarmona.Qna.Application.Initialization
{
    public class DatabaseInitializer
    {
        private readonly IAccountCommandRepository _accountRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly ILogger<DatabaseInitializer> _logger;

        public DatabaseInitializer(
            IAccountCommandRepository accountRepository, 
            IPasswordHasher passwordHasher, 
            IEventDispatcher eventDispatcher,
            ILogger<DatabaseInitializer> logger)
        {
            _accountRepository = accountRepository;
            _passwordHasher = passwordHasher;
            _eventDispatcher = eventDispatcher;
            _logger = logger;
        }

        public async Task SeedAdminUserAsync()
        {
            var adminUser = await _accountRepository.GetByNameAsync("admin");
            if (adminUser == null)
            {
                adminUser = new Account
                {
                    Username = "admin",
                    PasswordHash = _passwordHasher.Hash("P@ssw0rd!"),
                    Roles = ["Admin", "User"],
                    IsActive = true,
                    Profiles =
                    [
                        new UserProfile
                        {
                            FirstName = "Admin",
                            LastName = "User",
                            DisplayName = "Admin",
                        }
                    ]
                };

                await _accountRepository.AddAsync(adminUser);
                _logger.LogInformation("Admin user created successfully in SQL.");

                var accountCreatedEvent = new AccountCreatedEvent(
                    adminUser.Username,
                    adminUser.Email,
                    adminUser.Roles,
                    adminUser.IsActive,
                    adminUser.CreatedAt,
                    adminUser.TwoFactorEnabled,
                    adminUser.Profiles.Select(p => p.Id.ToString()).ToList()
                );

                await _eventDispatcher.DispatchAsync(accountCreatedEvent);
                _logger.LogInformation("Event dispatched for admin user creation.");
            }
        }
    }
}
