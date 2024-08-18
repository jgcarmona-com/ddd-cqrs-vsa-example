using NUlid;
using Jgcarmona.Qna.Domain.Entities;

namespace Jgcarmona.Qna.Application.Features.Users
{

    public interface IUserService
    {
        Task<User?> GetUserByIdAsync(Ulid id);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<User> CreateUserAsync(User newUser, string password);
        Task<User?> ValidateUserAsync(string username, string password);
        Task<bool> UpdateUserAsync(Ulid id, User updatedUser);
        Task<bool> DeleteUserAsync(Ulid id);
    }
}