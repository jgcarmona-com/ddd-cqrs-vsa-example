using NUlid;

namespace Jgcarmona.Qna.Domain.Views
{
    public abstract class BaseView
    {
        public string EntityId { get; set; } = Ulid.NewUlid().ToString();
        public string _id => $"{EntityId}_{Version}";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
        public int Version { get; set; } = 1;
    }
}
