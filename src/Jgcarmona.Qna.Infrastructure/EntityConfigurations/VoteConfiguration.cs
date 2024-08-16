using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Jgcarmona.Qna.Domain.Entities;

namespace Jgcarmona.Qna.Infrastructure.EntityConfigurations
{
    public class VoteConfiguration : BaseEntityConfiguration<Vote>
    {
        public override void Configure(EntityTypeBuilder<Vote> builder)
        {
            base.Configure(builder);
            builder.HasOne(v => v.Question)
                   .WithMany(q => q.Votes)
                   .HasForeignKey(v => v.QuestionId)
                   .OnDelete(DeleteBehavior.Restrict); // Evitar rutas de cascada múltiples

            builder.HasOne(v => v.User)
                   .WithMany(u => u.Votes)
                   .HasForeignKey(v => v.UserId)
                   .OnDelete(DeleteBehavior.Restrict); // Evitar rutas de cascada múltiples
        }
    }
}
