using Jgcarmona.Qna.Domain.Abstract.Repositories;
using Jgcarmona.Qna.Application.Services;
using Jgcarmona.Qna.Domain.Entities;
using Jgcarmona.Qna.Domain.Abstract.Services;

namespace Jgcarmona.Qna.Application.Initialization
{
    public class DatabaseInitializer
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public DatabaseInitializer(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task SeedAsync()
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
            }
        }
    }
}
