using Jgcarmona.Qna.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jgcarmona.Qna.Infrastructure.Persistence.Sql.EntityConfigurations
{
    public class UserProfileEntityConfiguration : BaseEntityConfiguration<UserProfile>
    {
        public override void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            base.Configure(builder);

            builder.Property(up => up.FirstName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(up => up.LastName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(up => up.DisplayName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(up => up.PhoneNumber)
                   .HasMaxLength(20);

            builder.Property(up => up.Gender)
                   .HasMaxLength(10);

            builder.Property(up => up.IsPrimary)
                   .IsRequired();

            builder.HasIndex(up => up.AccountId)
                   .IsUnique(false);

            builder.HasOne(up => up.Account)
                   .WithMany(a => a.Profiles)
                   .HasForeignKey(up => up.AccountId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(up => up.Questions)
                   .WithOne(q => q.Author)
                   .HasForeignKey(q => q.AuthorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(up => up.Answers)
                   .WithOne(a => a.Author)
                   .HasForeignKey(a => a.AuthorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(up => up.Votes)
                   .WithOne(v => v.Author)
                   .HasForeignKey(v => v.AuthorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(up => up.Comments)
                   .WithOne(c => c.Author)
                   .HasForeignKey(c => c.AuthorId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
