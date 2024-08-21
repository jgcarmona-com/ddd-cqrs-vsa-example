using MediatR;
using Jgcarmona.Qna.Domain.Abstract.Repositories;
using Jgcarmona.Qna.Application.Features.Users.Models;

namespace Jgcarmona.Qna.Application.Features.Users.Queries
{
    public class GetUserByUsernameQuery : IRequest<UserResponse>
    {
        public string Username { get; set; }

        public GetUserByUsernameQuery(string username)
        {
            Username = username;
        }
    }

    public class GetUserByUsernameQueryHandler : IRequestHandler<GetUserByUsernameQuery, UserResponse>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByUsernameQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponse> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUsernameAsync(request.Username);
            if (user == null)
            {
                return null; // Explicitly return null if the user isn't found
            }

            // Map the domain entity to the response model
            return new UserResponse
            {
                Username = user.Username,
                Role = user.Role
            };
        }
    }
}