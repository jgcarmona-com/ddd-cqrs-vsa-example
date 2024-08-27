using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Jgcarmona.Qna.Domain.Entities;

namespace Jgcarmona.Qna.Infrastructure.Persistence.Sql.EntityConfigurations
{
    public class UserConfiguration : BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.Property(u => u.Username).IsRequired().HasMaxLength(100);
            builder.HasIndex(u => u.Username).IsUnique();

            builder.Property(u => u.Role).IsRequired();

            builder.HasMany(u => u.Questions)
                   .WithOne(q => q.Author)
                   .HasForeignKey(q => q.AuthorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.Answers)
                   .WithOne(a => a.Author)
                   .HasForeignKey(a => a.AuthorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.Votes)
                   .WithOne(v => v.Author)
                   .HasForeignKey(v => v.AuthorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.Comments)
                   .WithOne(c => c.Author)
                   .HasForeignKey(c => c.AuthorId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
