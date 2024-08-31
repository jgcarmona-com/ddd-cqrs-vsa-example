using Jgcarmona.Qna.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jgcarmona.Qna.Infrastructure.Persistence.Sql.EntityConfigurations
{
    public class CommentConfiguration : BaseEntityConfiguration<Comment>
    {
        public override void Configure(EntityTypeBuilder<Comment> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.Content).IsRequired();

            builder.HasOne(c => c.Author)
                   .WithMany(a => a.Comments)
                   .HasForeignKey(c => c.AuthorId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.Question)
                    .WithMany(q => q.Comments)
                    .HasForeignKey(c => c.QuestionId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired(false);

            builder.HasOne(c => c.Answer)
                    .WithMany(a => a.Comments)
                    .HasForeignKey(c => c.AnswerId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(false);
        }
    }
}
