using DataPlatform.Infrastructure.Entities;
using DataPlatform.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataPlatform.Infrastructure.Repositories
{
    public class TranslationRepository : ITranslationRepository
    {
        private readonly AppDbContext _context;

        public TranslationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TranslationEntity>> GetAllAsync()
        {
            return await _context.Translations.ToListAsync();
        }

        public async Task<IEnumerable<TranslationEntity>> GetByCultureAsync(string culture)
        {
            return await _context.Translations
                .Where(t => t.Culture == culture)
                .ToListAsync();
        }

        public async Task<TranslationEntity?> GetByKeyAndCultureAsync(string resourceKey, string culture)
        {
            return await _context.Translations
                .FirstOrDefaultAsync(t => t.ResourceKey == resourceKey && t.Culture == culture);
        }

        public async Task<TranslationEntity?> GetByIdAsync(long id)
        {
            return await _context.Translations.FindAsync(id);
        }

        public async Task<TranslationEntity> CreateAsync(TranslationEntity translation)
        {
            _context.Translations.Add(translation);
            await _context.SaveChangesAsync();
            return translation;
        }

        public async Task<TranslationEntity> UpdateAsync(TranslationEntity translation)
        {
            _context.Translations.Update(translation);
            await _context.SaveChangesAsync();
            return translation;
        }

        public async Task DeleteAsync(long id)
        {
            var translation = await _context.Translations.FindAsync(id);
            if (translation != null)
            {
                _context.Translations.Remove(translation);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Dictionary<string, string>> GetTranslationDictionaryAsync(string culture)
        {
            return await _context.Translations
                .Where(t => t.Culture == culture)
                .ToDictionaryAsync(t => t.ResourceKey, t => t.Value);
        }
    }
}
