using DataPlatform.Application.DTOs;
using DataPlatform.Application.Interfaces;
using DataPlatform.Application.Mappers;
using DataPlatform.Infrastructure.Interfaces;

namespace DataPlatform.Application.Services
{
    public class LanguageService : ILanguageService
    {
        private readonly ILanguageRepository _languageRepository;

        public LanguageService(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        public async Task<IEnumerable<LanguageDto>> GetAllLanguagesAsync()
        {
            var entities = await _languageRepository.GetAllAsync();
            return entities.ToDto();
        }

        public async Task<IEnumerable<LanguageDto>> GetEnabledLanguagesAsync()
        {
            var entities = await _languageRepository.GetEnabledAsync();
            return entities.ToDto();
        }

        public async Task<LanguageDto?> GetLanguageAsync(string culture)
        {
            var entity = await _languageRepository.GetByCultureAsync(culture);
            return entity?.ToDto();
        }

        public async Task<LanguageDto> CreateLanguageAsync(CreateLanguageDto createDto)
        {
            var entity = createDto.ToEntity();
            var createdEntity = await _languageRepository.CreateAsync(entity);
            return createdEntity.ToDto();
        }

        public async Task<LanguageDto> UpdateLanguageAsync(long id, UpdateLanguageDto updateDto)
        {
            var entity = await _languageRepository.GetByIdAsync(id);
            if (entity == null)
                throw new ArgumentException($"Language with ID {id} not found");

            updateDto.UpdateEntity(entity);
            var updatedEntity = await _languageRepository.UpdateAsync(entity);
            return updatedEntity.ToDto();
        }

        public async Task DeleteLanguageAsync(long id)
        {
            await _languageRepository.DeleteAsync(id);
        }

        public async Task<bool> IsLanguageSupportedAsync(string culture)
        {
            return await _languageRepository.ExistsAsync(culture);
        }

        public async Task EnableLanguageAsync(string culture)
        {
            var language = await _languageRepository.GetByCultureAsync(culture);
            if (language != null)
            {
                language.Enabled = true;
                await _languageRepository.UpdateAsync(language);
            }
        }

        public async Task DisableLanguageAsync(string culture)
        {
            var language = await _languageRepository.GetByCultureAsync(culture);
            if (language != null)
            {
                language.Enabled = false;
                await _languageRepository.UpdateAsync(language);
            }
        }
    }
}
