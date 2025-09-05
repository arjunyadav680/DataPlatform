using DataPlatform.Application.DTOs;
using DataPlatform.Application.Interfaces;
using DataPlatform.Application.Mappers;
using DataPlatform.Infrastructure.Interfaces;

namespace DataPlatform.Application.Services
{
    public class TranslationService : ITranslationService
    {
        private readonly ITranslationRepository _translationRepository;

        public TranslationService(ITranslationRepository translationRepository)
        {
            _translationRepository = translationRepository;
        }

        public async Task<IEnumerable<TranslationDto>> GetAllTranslationsAsync()
        {
            var entities = await _translationRepository.GetAllAsync();
            return entities.ToDto();
        }

        public async Task<IEnumerable<TranslationDto>> GetTranslationsByCultureAsync(string culture)
        {
            var entities = await _translationRepository.GetByCultureAsync(culture);
            return entities.ToDto();
        }

        public async Task<TranslationDto?> GetTranslationAsync(string resourceKey, string culture)
        {
            var entity = await _translationRepository.GetByKeyAndCultureAsync(resourceKey, culture);
            return entity?.ToDto();
        }

        public async Task<TranslationDto> CreateTranslationAsync(CreateTranslationDto createDto)
        {
            var entity = createDto.ToEntity();
            var createdEntity = await _translationRepository.CreateAsync(entity);
            return createdEntity.ToDto();
        }

        public async Task<TranslationDto> UpdateTranslationAsync(long id, UpdateTranslationDto updateDto)
        {
            var entity = await _translationRepository.GetByIdAsync(id);
            if (entity == null)
                throw new ArgumentException($"Translation with ID {id} not found");

            updateDto.UpdateEntity(entity);
            var updatedEntity = await _translationRepository.UpdateAsync(entity);
            return updatedEntity.ToDto();
        }

        public async Task DeleteTranslationAsync(long id)
        {
            await _translationRepository.DeleteAsync(id);
        }

        public async Task<TranslationDictionaryDto> GetTranslationDictionaryAsync(string culture)
        {
            var translations = await _translationRepository.GetTranslationDictionaryAsync(culture);
            return new TranslationDictionaryDto
            {
                Culture = culture,
                Translations = translations
            };
        }

        public async Task<string> TranslateAsync(string resourceKey, string culture, string? fallbackValue = null)
        {
            var translation = await _translationRepository.GetByKeyAndCultureAsync(resourceKey, culture);
            
            if (translation != null)
                return translation.Value;

            // Fallback to English if not found in requested culture
            if (culture != "en")
            {
                var englishTranslation = await _translationRepository.GetByKeyAndCultureAsync(resourceKey, "en");
                if (englishTranslation != null)
                    return englishTranslation.Value;
            }

            // Return fallback value or resource key
            return fallbackValue ?? resourceKey;
        }
    }
}
