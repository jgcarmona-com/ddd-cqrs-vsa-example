using Jgcarmona.Qna.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jgcarmona.Qna.Infrastructure.Persistence.Sql.EntityConfigurations
{
    public class AnswerConfiguration : BaseEntityConfiguration<Answer>
    {
        public override void Configure(EntityTypeBuilder<Answer> builder)
        {
            base.Configure(builder);

            builder.Property(a => a.Content).IsRequired();

            builder.HasOne(a => a.Author)
                   .WithMany(p => p.Answers)
                   .HasForeignKey(a => a.AuthorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Question)
                   .WithMany(q => q.Answers)
                   .HasForeignKey(a => a.QuestionId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.Comments)
                   .WithOne(c => c.Answer)
                   .HasForeignKey(c => c.AnswerId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.Votes)
                   .WithOne(v => v.Answer)
                   .HasForeignKey(v => v.AnswerId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
