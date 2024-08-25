using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Jgcarmona.Qna.Domain.Entities;
using Jgcarmona.Qna.Infrastructure.Persistence.Sql.EntityConfigurations;

public class QuestionConfiguration : BaseEntityConfiguration<Question>
{
    public override void Configure(EntityTypeBuilder<Question> builder)
    {
        base.Configure(builder);
        builder.Property(q => q.Title).IsRequired().HasMaxLength(200);
        builder.HasIndex(q => q.Moniker).IsUnique();
    }
}
