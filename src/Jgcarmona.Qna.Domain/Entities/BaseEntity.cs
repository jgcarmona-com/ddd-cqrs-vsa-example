using NUlid;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jgcarmona.Qna.Domain.Entities;

public abstract class BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Ulid Id { get; set; } = Ulid.NewUlid();

    public bool IsDeleted { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public void MarkAsDeleted()
    {
        IsDeleted = true;
    }
}
