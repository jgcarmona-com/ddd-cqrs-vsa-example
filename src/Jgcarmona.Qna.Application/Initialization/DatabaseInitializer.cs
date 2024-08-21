using Jgcarmona.Qna.Domain.Abstract.Repositories;
using Jgcarmona.Qna.Domain.Entities;
using Jgcarmona.Qna.Domain.Abstract.Services;
using Microsoft.Extensions.Logging;

namespace Jgcarmona.Qna.Application.Initialization
{
    public class DatabaseInitializer
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ILogger<DatabaseInitializer> _logger;

        public DatabaseInitializer(IUserRepository userRepository, IPasswordHasher passwordHasher, ILogger<DatabaseInitializer> logger)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _logger = logger;
        }

        public async Task SeedAdminUserAsync()
        {
            if (!(await _userRepository.GetAllAsync()).Any())
            {
                var adminUser = new User
                {
                    Username = "admin",
                    PasswordHash = _passwordHasher.Hash("P@ssw0rd!"),
                    Role = "Admin"
                };

                await _userRepository.AddAsync(adminUser);
                _logger.LogInformation("Admin user created successfully.");
            }
        }
    }
}
