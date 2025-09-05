using DataPlatform.Infrastructure.Entities;
using DataPlatform.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataPlatform.Infrastructure.Repositories
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly AppDbContext _context;

        public LanguageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LanguageEntity>> GetAllAsync()
        {
            return await _context.Languages
                .OrderBy(l => l.Priority)
                .ToListAsync();
        }

        public async Task<IEnumerable<LanguageEntity>> GetEnabledAsync()
        {
            return await _context.Languages
                .Where(l => l.Enabled)
                .OrderBy(l => l.Priority)
                .ToListAsync();
        }

        public async Task<LanguageEntity?> GetByCultureAsync(string culture)
        {
            return await _context.Languages
                .FirstOrDefaultAsync(l => l.Culture == culture);
        }

        public async Task<LanguageEntity?> GetByIdAsync(long id)
        {
            return await _context.Languages.FindAsync(id);
        }

        public async Task<LanguageEntity> CreateAsync(LanguageEntity language)
        {
            _context.Languages.Add(language);
            await _context.SaveChangesAsync();
            return language;
        }

        public async Task<LanguageEntity> UpdateAsync(LanguageEntity language)
        {
            _context.Languages.Update(language);
            await _context.SaveChangesAsync();
            return language;
        }

        public async Task DeleteAsync(long id)
        {
            var language = await _context.Languages.FindAsync(id);
            if (language != null)
            {
                _context.Languages.Remove(language);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(string culture)
        {
            return await _context.Languages.AnyAsync(l => l.Culture == culture);
        }
    }
}
