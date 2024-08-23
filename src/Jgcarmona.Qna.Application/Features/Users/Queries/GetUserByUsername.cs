using MediatR;
using Jgcarmona.Qna.Domain.Abstract.Repositories;
using Jgcarmona.Qna.Application.Features.Users.Models;
using Jgcarmona.Qna.Domain.Abstract.Events;
using Microsoft.AspNetCore.Http;
using Jgcarmona.Qna.Application.Features.Users.Commands.RegisterUser;
using Microsoft.Extensions.Logging;
using Jgcarmona.Qna.Domain.Events;

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
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<GetUserByUsernameQueryHandler> _logger;

        public GetUserByUsernameQueryHandler(
            IUserRepository userRepository,
            ILogger<GetUserByUsernameQueryHandler> logger,
            IEventDispatcher eventDispatcher,
            IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _eventDispatcher = eventDispatcher;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<UserResponse> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUsernameAsync(request.Username);
            if (user == null)
            {
                return null; // Explicitly return null if the user isn't found
            }

            // Get the correlation ID from the HTTP context
            var correlationId = _httpContextAccessor.HttpContext?.Items["CorrelationId"]?.ToString() ?? string.Empty;

            // Dispatch the event
            var userRegisteredEvent = new UserViewedEvent(user.Id, user.Username, user.Role)
            {
                CorrelationId = correlationId
            };

            await _eventDispatcher.DispatchAsync(userRegisteredEvent);

            // Map the domain entity to the response model
            return new UserResponse
            {
                Username = user.Username,
                Role = user.Role
            };
        }
    }
}