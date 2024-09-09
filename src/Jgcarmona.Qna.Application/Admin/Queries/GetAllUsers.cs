using Jgcarmona.Qna.Application.Accounts.Models;
using Jgcarmona.Qna.Domain.Repositories.Query;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Jgcarmona.Qna.Application.Admin.Queries
{
    public class GetAllUsersQuery : IRequest<List<AccountModel>>
    {
    }

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<AccountModel>>
    {
        private readonly IAccountViewQueryRepository _userQueryRepository;
        private readonly ILogger<GetAllUsersQueryHandler> _logger;

        public GetAllUsersQueryHandler(IAccountViewQueryRepository userQueryRepository, ILogger<GetAllUsersQueryHandler> logger)
        {
            _userQueryRepository = userQueryRepository;
            _logger = logger;
        }

        public async Task<List<AccountModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userQueryRepository.GetAllAsync();
            if (users == null || !users.Any())
            {
                throw new Exception("No users found");
            }

            return users.Select(u => AccountModel.FromView(u)).ToList();
        }
    }
}
