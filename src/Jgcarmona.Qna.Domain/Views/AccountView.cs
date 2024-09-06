namespace Jgcarmona.Qna.Domain.Views
{
    public class AccountView : BaseView
    {
        public string LoginName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new();
        public bool IsActive { get; set; } = true;
        public bool EmailVerified { get; set; } = false;
        public DateTime? LastLoginDate { get; set; }
        public bool TwoFactorEnabled { get; set; } = false;
        public string PrimaryProfileId { get; set; } = string.Empty;
        public List<string> ProfileIds { get; set; } = new();

        public Guid? VerificationTokenId { get; set; }
        public DateTime? TokenCreatedOnUtc { get; set; }
        public DateTime? TokenExpiresOnUtc { get; set; }
    }
}
