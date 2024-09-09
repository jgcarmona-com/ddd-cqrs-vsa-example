using Jgcarmona.Qna.Application.UserProfiles.Models;
using Jgcarmona.Qna.Domain.Entities;
using Jgcarmona.Qna.Domain.Events;
using Jgcarmona.Qna.Domain.Repositories.Command;
using Jgcarmona.Qna.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NUlid;

namespace Jgcarmona.Qna.Application.UserProfiles.Commands
{
    public class UpdateUserProfileCommand : IRequest<UserProfileModel>
    {
        public Ulid UserProfileId { get; set; }
        public UpdateUserProfileModel Model { get; set; }

        public UpdateUserProfileCommand(Ulid userProfileId, UpdateUserProfileModel model)
        {
            UserProfileId = userProfileId;
            Model = model;
        }
    }

    public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, UserProfileModel>
    {
        private readonly ICommandRepository<UserProfile> _userProfileRepository;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<UpdateUserProfileCommandHandler> _logger;

        public UpdateUserProfileCommandHandler(
            ICommandRepository<UserProfile> userProfileRepository,
            IEventDispatcher eventDispatcher,
            IHttpContextAccessor httpContextAccessor,
            ILogger<UpdateUserProfileCommandHandler> logger)
        {
            _userProfileRepository = userProfileRepository;
            _eventDispatcher = eventDispatcher;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<UserProfileModel> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var profile = await _userProfileRepository.GetByIdAsync(request.UserProfileId);
            if (profile == null)
            {
                throw new Exception($"Profile with ID {request.UserProfileId} not found");
            }

            profile.FirstName = request.Model.FirstName;
            profile.LastName = request.Model.LastName;
            profile.DisplayName = request.Model.DisplayName;
            profile.DateOfBirth = request.Model.DateOfBirth;
            profile.ProfilePictureUrl = request.Model.ProfilePictureUrl;
            profile.PhoneNumber = request.Model.PhoneNumber;
            profile.Gender = request.Model.Gender;

            await _userProfileRepository.UpdateAsync(profile);

            // TODO: Dispatch event
            var correlationId = _httpContextAccessor.HttpContext?.Items["CorrelationId"]?.ToString() ?? string.Empty;

            var questionCreatedEvent = new UserProfileUpdatedEvent(profile)
            {
                CorrelationId = correlationId
            };
            await _eventDispatcher.DispatchAsync(questionCreatedEvent);
            _logger.LogInformation($"User Profile Updated Event dispatched for profile with ID {profile.Id}.");
            return UserProfileModel.FromEntity(profile);
        }
    }
}
