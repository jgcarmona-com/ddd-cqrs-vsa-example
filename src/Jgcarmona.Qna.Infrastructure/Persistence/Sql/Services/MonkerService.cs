using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Jgcarmona.Qna.Domain.Entities;
using Jgcarmona.Qna.Domain.Services;

namespace Jgcarmona.Qna.Infrastructure.Persistence.Sql.Services
{
    public class MonikerService : IMonikerService
    {
        private readonly ApplicationDbContext _context;

        public MonikerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> GenerateMonikerAsync<T>(string baseText) where T : IdentifiableEntity
        {
            var moniker = GenerateSlug(baseText);
            var existingCount = await _context.Set<T>().CountAsync(e => e.Moniker.StartsWith(moniker));

            if (existingCount > 0)
            {
                moniker = $"{moniker}-{existingCount + 1}";
            }

            return moniker;
        }

        private string GenerateSlug(string text)
        {
           // TODO: Implement more robust slug generation logic
            return text.ToLower().Replace(" ", "-");
        }
    }
}
