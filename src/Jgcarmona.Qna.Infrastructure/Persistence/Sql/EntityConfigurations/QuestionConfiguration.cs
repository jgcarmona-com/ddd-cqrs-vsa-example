using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Jgcarmona.Qna.Domain.Entities;
using Jgcarmona.Qna.Infrastructure.Persistence.Sql.EntityConfigurations;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public class QuestionConfiguration : BaseEntityConfiguration<Question>
{
    public override void Configure(EntityTypeBuilder<Question> builder)
    {
        base.Configure(builder);

        builder.Property(q => q.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(q => q.Content)
            .IsRequired();

        builder.HasOne(q => q.Author)
            .WithMany(u => u.Questions) 
            .HasForeignKey(q => q.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        var tagsValueComparer = new ValueComparer<List<string>>(
            (c1, c2) => c1.SequenceEqual(c2),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c => c.ToList()
        );

        builder.Property(q => q.Tags)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
            )
            .Metadata.SetValueComparer(tagsValueComparer);

        builder.HasMany(q => q.Answers)
            .WithOne(a => a.Question) 
            .HasForeignKey(a => a.QuestionId) 
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(q => q.Moniker).IsUnique();
    }
}
