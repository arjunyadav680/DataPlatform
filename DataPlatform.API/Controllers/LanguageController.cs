using DataPlatform.Application.DTOs;
using DataPlatform.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DataPlatform.API.Controllers
{
    [Route("languages")]
    public class LanguageController : BaseController
    {
        private readonly ILanguageService _languageService;

        public LanguageController(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LanguageDto>>> GetAllLanguages()
        {
            var languages = await _languageService.GetAllLanguagesAsync();
            var languageDtos = languages.Select(l => new LanguageDto
            {
                Id = l.Id,
                Culture = l.Culture,
                DisplayName = l.DisplayName,
                NativeName = l.NativeName,
                Direction = l.Direction,
                Enabled = l.Enabled,
                Priority = l.Priority,
                Source = l.Source,
                CreatedAt = l.CreatedAt,
                CreatedBy = l.CreatedBy,
                UpdatedAt = l.UpdatedAt,
                UpdatedBy = l.UpdatedBy
            });

            return Ok(languageDtos);
        }

        [HttpGet("enabled")]
        public async Task<ActionResult<IEnumerable<LanguageDto>>> GetEnabledLanguages()
        {
            var languages = await _languageService.GetEnabledLanguagesAsync();
            var languageDtos = languages.Select(l => new LanguageDto
            {
                Id = l.Id,
                Culture = l.Culture,
                DisplayName = l.DisplayName,
                NativeName = l.NativeName,
                Direction = l.Direction,
                Enabled = l.Enabled,
                Priority = l.Priority,
                Source = l.Source,
                CreatedAt = l.CreatedAt,
                CreatedBy = l.CreatedBy,
                UpdatedAt = l.UpdatedAt,
                UpdatedBy = l.UpdatedBy
            });

            return Ok(languageDtos);
        }

        [HttpGet("{culture}")]
        public async Task<ActionResult<LanguageDto>> GetLanguage(string culture)
        {
            var language = await _languageService.GetLanguageAsync(culture);
            
            if (language == null)
                return NotFound($"Language not found for culture '{culture}'");

            var languageDto = new LanguageDto
            {
                Id = language.Id,
                Culture = language.Culture,
                DisplayName = language.DisplayName,
                NativeName = language.NativeName,
                Direction = language.Direction,
                Enabled = language.Enabled,
                Priority = language.Priority,
                Source = language.Source,
                CreatedAt = language.CreatedAt,
                CreatedBy = language.CreatedBy,
                UpdatedAt = language.UpdatedAt,
                UpdatedBy = language.UpdatedBy
            };

            return Ok(languageDto);
        }

        [HttpPost]
        public async Task<ActionResult<LanguageDto>> CreateLanguage([FromBody] CreateLanguageDto createDto)
        {
            var language = await _languageService.CreateLanguageAsync(createDto);

            return CreatedAtAction(nameof(GetLanguage), 
                new { culture = language.Culture }, 
                language);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLanguage(long id)
        {
            await _languageService.DeleteLanguageAsync(id);
            return NoContent();
        }

        [HttpPost("{culture}/enable")]
        public async Task<ActionResult> EnableLanguage(string culture)
        {
            await _languageService.EnableLanguageAsync(culture);
            return Ok();
        }

        [HttpPost("{culture}/disable")]
        public async Task<ActionResult> DisableLanguage(string culture)
        {
            await _languageService.DisableLanguageAsync(culture);
            return Ok();
        }

        [HttpGet("{culture}/supported")]
        public async Task<ActionResult<bool>> IsLanguageSupported(string culture)
        {
            var isSupported = await _languageService.IsLanguageSupportedAsync(culture);
            return Ok(isSupported);
        }
    }
}
