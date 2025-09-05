using DataPlatform.Application.DTOs;

namespace DataPlatform.Application.Interfaces
{
    public interface ITranslationService
    {
        Task<IEnumerable<TranslationDto>> GetAllTranslationsAsync();
        Task<IEnumerable<TranslationDto>> GetTranslationsByCultureAsync(string culture);
        Task<TranslationDto?> GetTranslationAsync(string resourceKey, string culture);
        Task<TranslationDto> CreateTranslationAsync(CreateTranslationDto createDto);
        Task<TranslationDto> UpdateTranslationAsync(long id, UpdateTranslationDto updateDto);
        Task DeleteTranslationAsync(long id);
        Task<TranslationDictionaryDto> GetTranslationDictionaryAsync(string culture);
        Task<string> TranslateAsync(string resourceKey, string culture, string? fallbackValue = null);
    }
}
