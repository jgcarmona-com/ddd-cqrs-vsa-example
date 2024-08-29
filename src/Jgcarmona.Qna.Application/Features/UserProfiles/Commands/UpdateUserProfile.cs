using Jgcarmona.Qna.Application.Features.UserProfiles.Models;
using Jgcarmona.Qna.Domain.Abstract.Repositories.Command;
using Jgcarmona.Qna.Domain.Entities;
using MediatR;
using NUlid;

namespace Jgcarmona.Qna.Application.Features.UserProfiles.Commands
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

        public UpdateUserProfileCommandHandler(ICommandRepository<UserProfile> userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
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
            return UserProfileModel.FromEntity(profile);
        }
    }
}
