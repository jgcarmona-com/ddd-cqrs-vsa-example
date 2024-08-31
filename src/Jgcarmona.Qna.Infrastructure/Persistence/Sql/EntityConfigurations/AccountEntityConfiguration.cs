using Jgcarmona.Qna.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Jgcarmona.Qna.Infrastructure.Persistence.Sql.EntityConfigurations
{
    public class AccountEntityConfiguration : BaseEntityConfiguration<Account>
    {
        public override void Configure(EntityTypeBuilder<Account> builder)
        {
            base.Configure(builder);

            builder.Property(a => a.LoginName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasIndex(a => a.LoginName)
                   .IsUnique();

            builder.Property(a => a.Email)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(a => a.PasswordHash)
                   .IsRequired();

            builder.Property(a => a.IsActive)
                   .IsRequired();

            builder.Property(a => a.LastLoginDate)
                   .IsRequired(false);

            builder.Property(a => a.PasswordResetToken)
                   .HasMaxLength(255)
                   .IsRequired(false);

            builder.Property(a => a.PasswordResetExpiration)
                   .IsRequired(false);

            builder.Property(a => a.TwoFactorEnabled)
                   .IsRequired();

            var rolesConverter = new ValueConverter<List<string>, string>(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());

            var rolesComparer = new ValueComparer<List<string>>(
                (c1, c2) => c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList());

            builder.Property(a => a.Roles)
                   .HasConversion(rolesConverter)
                   .Metadata.SetValueComparer(rolesComparer);

            builder.HasMany(a => a.Profiles)
                   .WithOne(p => p.Account)
                   .HasForeignKey(p => p.AccountId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
