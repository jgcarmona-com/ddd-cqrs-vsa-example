using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Jgcarmona.Qna.Domain.Entities;

namespace Jgcarmona.Qna.Infrastructure.EntityConfigurations
{
    public class AnswerConfiguration : BaseEntityConfiguration<Answer>
    {
        public override void Configure(EntityTypeBuilder<Answer> builder)
        {
            base.Configure(builder);
            builder.Property(a => a.Content).IsRequired();
            builder.HasOne(a => a.Question)
                   .WithMany(q => q.Answers)
                   .HasForeignKey(a => a.QuestionId)
                   .OnDelete(DeleteBehavior.Restrict); // Evitar rutas de cascada múltiples

            builder.HasOne(a => a.User)
                   .WithMany(u => u.Answers)
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.Restrict); // Evitar rutas de cascada múltiples
        }
    }
}
