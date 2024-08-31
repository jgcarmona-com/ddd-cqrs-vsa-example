using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Jgcarmona.Qna.Domain.Entities;
using Jgcarmona.Qna.Domain.Services;
using Microsoft.EntityFrameworkCore;

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
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            // Convert to lower case
            var slug = text.ToLowerInvariant();

            // Remove diacritics (accents, etc.)
            slug = RemoveDiacritics(slug);

            // Remove invalid characters
            slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");

            // Replace spaces with hyphens
            slug = Regex.Replace(slug, @"\s+", "-").Trim('-');

            // Remove any duplicated hyphens
            slug = Regex.Replace(slug, @"-+", "-");

            // Optionally, limit the length of the slug (e.g., to 100 characters)
            if (slug.Length > 100)
            {
                slug = slug.Substring(0, 100).Trim('-');
            }

            return slug;
        }

        private static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
