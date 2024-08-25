using Microsoft.EntityFrameworkCore;
using Jgcarmona.Qna.Domain.Entities;
using NUlid;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;


namespace Jgcarmona.Qna.Infrastructure.Persistence.Sql
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Vote> Votes { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var ulidConverter = new ValueConverter<Ulid, string>(
                v => v.ToString(), // Ulid to string
                v => Ulid.Parse(v)); // string to Ulid

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entityType.ClrType.GetProperties()
                    .Where(p => p.PropertyType == typeof(Ulid));

                foreach (var property in properties)
                {
                    modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion(ulidConverter);
                }
            }
            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
