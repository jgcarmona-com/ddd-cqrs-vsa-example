namespace Jgcarmona.Qna.Domain.Events;

public class AccountCreatedEvent : EventBase
{
    public string Name { get; }
    public string Email { get; }
    public List<string> Roles { get; }
    public bool IsActive { get; }
    public DateTime CreatedAt { get; }
    public bool TwoFactorEnabled { get; }
    public List<string> ProfileIds { get; }

    public AccountCreatedEvent(string name, string email, List<string> roles, bool isActive, DateTime createdAt, bool twoFactorEnabled, List<string> profileIds)
    {
        Name = name;
        Email = email;
        Roles = roles;
        IsActive = isActive;
        CreatedAt = createdAt;
        TwoFactorEnabled = twoFactorEnabled;
        ProfileIds = profileIds;
    }
}
