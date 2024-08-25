namespace Jgcarmona.Qna.Domain.Entities;

public abstract class IdentifiableEntity : BaseEntity
{
    public string Moniker { get; set; } = string.Empty;

    public void SetMoniker(string moniker)
    {
        if (string.IsNullOrWhiteSpace(moniker))
        {
            throw new ArgumentException("Moniker cannot be empty or null.", nameof(moniker));
        }

        Moniker = moniker;
    }
}
