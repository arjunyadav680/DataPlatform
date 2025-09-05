using DataPlatform.Infrastructure.Entities;

namespace DataPlatform.Infrastructure.Interfaces
{
    public interface ILanguageRepository
    {
        Task<IEnumerable<LanguageEntity>> GetAllAsync();
        Task<IEnumerable<LanguageEntity>> GetEnabledAsync();
        Task<LanguageEntity?> GetByCultureAsync(string culture);
        Task<LanguageEntity?> GetByIdAsync(long id);
        Task<LanguageEntity> CreateAsync(LanguageEntity language);
        Task<LanguageEntity> UpdateAsync(LanguageEntity language);
        Task DeleteAsync(long id);
        Task<bool> ExistsAsync(string culture);
    }
}
