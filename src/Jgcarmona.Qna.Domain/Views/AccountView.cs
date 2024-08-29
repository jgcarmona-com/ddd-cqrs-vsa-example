namespace Jgcarmona.Qna.Domain.Views
{
    public class AccountView : BaseView
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new();
        public bool IsActive { get; set; } = true;
        public DateTime? LastLoginDate { get; set; }
        public bool TwoFactorEnabled { get; set; } = false;
        public List<string> ProfileIds { get; set; } = new();
    }
}
