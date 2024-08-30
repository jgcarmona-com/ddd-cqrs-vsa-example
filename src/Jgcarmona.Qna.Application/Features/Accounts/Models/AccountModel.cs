
using global::Jgcarmona.Qna.Domain.Entities;
using Jgcarmona.Qna.Domain.Entities;
using Jgcarmona.Qna.Domain.Views;
using System.Data;

namespace Jgcarmona.Qna.Application.Features.Admin.Models
{
    public class AccountModel
    {
        public string Id { get; set; }
        public string LoginName { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new List<string>();
        public bool IsActive { get; set; }

        public static AccountModel FromEntity(Account account)
        {
            return new AccountModel
            {
                Id = account.Id.ToString(),
                LoginName = account.LoginName,
                Roles = account.Roles,
                IsActive = account.IsActive
            };
        }

        public static AccountModel FromView(AccountView view)
        {
            return new AccountModel
            {
                Id = view.EntityId.ToString(),
                LoginName = view.LoginName,
                Roles = view.Roles,
                IsActive = view.IsActive
            };
        }
    }
}