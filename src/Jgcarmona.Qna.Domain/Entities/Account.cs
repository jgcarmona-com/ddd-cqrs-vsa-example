using NUlid;
using System.Text.Json.Serialization;

namespace Jgcarmona.Qna.Domain.Entities
{
    public class Account: BaseEntity
    {
        public string LoginName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = [];
        public bool IsActive { get; set; } = true;
        public DateTime? LastLoginDate { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? PasswordResetExpiration { get; set; }
        public bool TwoFactorEnabled { get; set; } = false;
        public List<UserProfile> Profiles { get; set; } = new List<UserProfile>();

        public void AddRole(string role)
        {
            if (!Roles.Contains(role))
            {
                Roles.Add(role);
            }
        }

        public void RemoveRole(string role)
        {
            if (Roles.Contains(role))
            {
                Roles.Remove(role);
            }
        }

        public bool HasRole(string role)
        {
            return Roles.Contains(role);
        }
    }
}

