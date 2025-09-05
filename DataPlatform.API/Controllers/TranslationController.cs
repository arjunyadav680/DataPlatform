using DataPlatform.Application.DTOs;
using DataPlatform.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DataPlatform.API.Controllers
{
    [Route("translations")]
 
    public class TranslationController : BaseController
    {
        private readonly ITranslationService _translationService;

        public TranslationController(ITranslationService translationService)
        {
            _translationService = translationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TranslationDto>>> GetAllTranslations()
        {
            var translations = await _translationService.GetAllTranslationsAsync();
            var translationDtos = translations.Select(t => new TranslationDto
            {
                Id = t.Id,
                ResourceKey = t.ResourceKey,
                Culture = t.Culture,
                Value = t.Value,
                Context = t.Context,
                Source = t.Source,
                Version = t.Version,
                CreatedAt = t.CreatedAt,
                CreatedBy = t.CreatedBy,
                UpdatedAt = t.UpdatedAt,
                UpdatedBy = t.UpdatedBy
            });

            return Ok(translationDtos);
        }

        [HttpGet("culture/{culture}")]
        public async Task<ActionResult<IEnumerable<TranslationDto>>> GetTranslationsByCulture(string culture)
        {
            var translations = await _translationService.GetTranslationsByCultureAsync(culture);
            var translationDtos = translations.Select(t => new TranslationDto
            {
                Id = t.Id,
                ResourceKey = t.ResourceKey,
                Culture = t.Culture,
                Value = t.Value,
                Context = t.Context,
                Source = t.Source,
                Version = t.Version,
                CreatedAt = t.CreatedAt,
                CreatedBy = t.CreatedBy,
                UpdatedAt = t.UpdatedAt,
                UpdatedBy = t.UpdatedBy
            });

            return Ok(translationDtos);
        }

        [HttpGet("culture/{culture}/dictionary")]
        public async Task<ActionResult<TranslationDictionaryDto>> GetTranslationDictionary(string culture)
        {
            var result = await _translationService.GetTranslationDictionaryAsync(culture);
            return Ok(result);
        }

        [HttpGet("{resourceKey}/culture/{culture}")]
        public async Task<ActionResult<TranslationDto>> GetTranslation(string resourceKey, string culture)
        {
            var translation = await _translationService.GetTranslationAsync(resourceKey, culture);

            if (translation == null)
                return NotFound($"Translation not found for key '{resourceKey}' in culture '{culture}'");

            var translationDto = new TranslationDto
            {
                Id = translation.Id,
                ResourceKey = translation.ResourceKey,
                Culture = translation.Culture,
                Value = translation.Value,
                Context = translation.Context,
                Source = translation.Source,
                Version = translation.Version,
                CreatedAt = translation.CreatedAt,
                CreatedBy = translation.CreatedBy,
                UpdatedAt = translation.UpdatedAt,
                UpdatedBy = translation.UpdatedBy
            };

            return Ok(translationDto);
        }

        [HttpPost]
        public async Task<ActionResult<TranslationDto>> CreateTranslation([FromBody] CreateTranslationDto createDto)
        {
            var translation = await _translationService.CreateTranslationAsync(createDto);

            return CreatedAtAction(nameof(GetTranslation), 
                new { resourceKey = translation.ResourceKey, culture = translation.Culture }, 
                translation);
        }        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTranslation(long id)
        {
            await _translationService.DeleteTranslationAsync(id);
            return NoContent();
        }

        [HttpGet("translate")]
        public async Task<ActionResult<string>> Translate([FromQuery] string resourceKey, [FromQuery] string culture, [FromQuery] string? fallback = null)
        {
            var translation = await _translationService.TranslateAsync(resourceKey, culture, fallback);
            return Ok(translation);
        }
    }
}
