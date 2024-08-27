using NUlid;

namespace Jgcarmona.Qna.Domain.Views
{
    public abstract class BaseView
    {
        public string Id { get; set; } = Ulid.NewUlid().ToString();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
    }
}
