using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Jgcarmona.Qna.Domain.Entities;
using NUlid;

namespace Jgcarmona.Qna.Infrastructure.Persistence.Sql.EntityConfigurations
{
    public class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
           builder.HasKey(e => e.Id);

            // Configure Ulid as primary key
            builder.Property(e => e.Id)
                .HasConversion(
                    ulid => ulid.ToString(), // From Ulid to string
                    ulidString => Ulid.Parse(ulidString) // From string to Ulid
                );

            // Filter out deleted entities
            builder.HasQueryFilter(e => !e.IsDeleted);

            builder.Property(e => e.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
