using DataPlatform.Infrastructure.Entities;

namespace DataPlatform.Infrastructure.Interfaces
{
    public interface ITranslationRepository
    {
        Task<IEnumerable<TranslationEntity>> GetAllAsync();
        Task<IEnumerable<TranslationEntity>> GetByCultureAsync(string culture);
        Task<TranslationEntity?> GetByKeyAndCultureAsync(string resourceKey, string culture);
        Task<TranslationEntity?> GetByIdAsync(long id);
        Task<TranslationEntity> CreateAsync(TranslationEntity translation);
        Task<TranslationEntity> UpdateAsync(TranslationEntity translation);
        Task DeleteAsync(long id);
        Task<Dictionary<string, string>> GetTranslationDictionaryAsync(string culture);
    }
}
