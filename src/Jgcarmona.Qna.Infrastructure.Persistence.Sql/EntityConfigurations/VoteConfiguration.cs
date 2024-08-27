using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Jgcarmona.Qna.Domain.Entities;

namespace Jgcarmona.Qna.Infrastructure.Persistence.Sql.EntityConfigurations
{
    public class VoteConfiguration : BaseEntityConfiguration<Vote>
    {
        public override void Configure(EntityTypeBuilder<Vote> builder)
        {
            base.Configure(builder);

            builder.Property(v => v.IsUpvote).IsRequired();

            builder.HasOne(v => v.Question)
                   .WithMany(q => q.Votes)
                   .HasForeignKey(v => v.QuestionId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(v => v.Author)
                   .WithMany(u => u.Votes)
                   .HasForeignKey(v => v.AuthorId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
