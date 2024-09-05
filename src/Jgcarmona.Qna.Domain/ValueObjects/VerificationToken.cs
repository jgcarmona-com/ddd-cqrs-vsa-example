namespace Jgcarmona.Qna.Domain.ValueObjects;

public sealed class VerificationToken : ValueObject
{
    public Guid Id { get; }
    public DateTime CreatedOnUtc { get; }
    public DateTime ExpiresOnUtc { get; }

    private VerificationToken(Guid id, DateTime createdOnUtc, DateTime expiresOnUtc)
    {
        Id = id;
        CreatedOnUtc = createdOnUtc;
        ExpiresOnUtc = expiresOnUtc;
    }

    public static VerificationToken Create()
    {
        var utcNow = DateTime.UtcNow;
        return new VerificationToken(
            Guid.NewGuid(),
            utcNow,
            utcNow.AddDays(1)); // It expires in 1 day
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Id;
        yield return CreatedOnUtc;
        yield return ExpiresOnUtc;
    }
}