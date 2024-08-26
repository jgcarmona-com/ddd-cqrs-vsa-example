using Jgcarmona.Qna.Domain.Abstract.Repositories;
using Jgcarmona.Qna.Domain.Entities;
using Jgcarmona.Qna.Domain.Abstract.Services;
using Microsoft.Extensions.Logging;
using Jgcarmona.Qna.Domain.Abstract.Events;
using Jgcarmona.Qna.Domain.Events;

namespace Jgcarmona.Qna.Application.Initialization
{
    public class DatabaseInitializer
    {
        private readonly IUserCommandRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly ILogger<DatabaseInitializer> _logger;

        public DatabaseInitializer(
            IUserCommandRepository userRepository, 
            IPasswordHasher passwordHasher, 
            IEventDispatcher eventDispatcher,
            ILogger<DatabaseInitializer> logger)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _eventDispatcher = eventDispatcher;
            _logger = logger;
        }

        public async Task SeedAdminUserAsync()
        {
            var adminUser = await _userRepository.GetByUsernameAsync("admin");
            if (adminUser == null) {
                adminUser = new User
                {
                    Username = "admin",
                    PasswordHash = _passwordHasher.Hash("P@ssw0rd!"),
                    Role = "Admin"
                };

                await _userRepository.AddAsync(adminUser);
                _logger.LogInformation("Admin user created successfully in SQL.");

                await _eventDispatcher.DispatchAsync(new UserRegisteredEvent(adminUser));
                _logger.LogInformation("Event dispatched for admin user creation.");
            }
        }
    }
}
