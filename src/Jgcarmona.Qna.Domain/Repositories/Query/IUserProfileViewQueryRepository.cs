﻿using Jgcarmona.Qna.Domain.Views;

namespace Jgcarmona.Qna.Domain.Repositories.Query
{
    public interface IUserProfileViewQueryRepository : IQueryRepository<UserProfileView>
    {
        Task<UserProfileView?> GetByDisplayNameAsync(string displayName);
        Task<IEnumerable<UserProfileView>> GetByAccountIdAsync(string accountId);
    }
}
