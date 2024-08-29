using Jgcarmona.Qna.Application.Features.UserProfiles.Models;
using Jgcarmona.Qna.Domain.Abstract.Repositories.Query;
using Jgcarmona.Qna.Domain.Views;
using MediatR;
using NUlid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jgcarmona.Qna.Application.Features.UserProfiles.Queries
{
    public class GetUserProfileByIdQuery : IRequest<UserProfileModel>
    {
        public Ulid UserProfileId { get; }

        public GetUserProfileByIdQuery(Ulid userProfileId)
        {
            UserProfileId = userProfileId;
        }
    }

    public class GetUserProfileByIdQueryHandler : IRequestHandler<GetUserProfileByIdQuery, UserProfileModel>
    {
        private readonly IUserProfileViewQueryRepository _userProfileRepository;

        public GetUserProfileByIdQueryHandler(IUserProfileViewQueryRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        public async Task<UserProfileModel> Handle(GetUserProfileByIdQuery request, CancellationToken cancellationToken)
        {
            var profile = await _userProfileRepository.GetByIdAsync(request.UserProfileId);

            if (profile == null)
            {
                throw new Exception($"Profile with ID {request.UserProfileId} not found");
            }

            return UserProfileModel.FromView(profile);
        }
    }

}
