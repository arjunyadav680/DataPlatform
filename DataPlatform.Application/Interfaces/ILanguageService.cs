using DataPlatform.Application.DTOs;

namespace DataPlatform.Application.Interfaces
{
    public interface ILanguageService
    {
        Task<IEnumerable<LanguageDto>> GetAllLanguagesAsync();
        Task<IEnumerable<LanguageDto>> GetEnabledLanguagesAsync();
        Task<LanguageDto?> GetLanguageAsync(string culture);
        Task<LanguageDto> CreateLanguageAsync(CreateLanguageDto createDto);
        Task<LanguageDto> UpdateLanguageAsync(long id, UpdateLanguageDto updateDto);
        Task DeleteLanguageAsync(long id);
        Task<bool> IsLanguageSupportedAsync(string culture);
        Task EnableLanguageAsync(string culture);
        Task DisableLanguageAsync(string culture);
    }
}
