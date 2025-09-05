using DataPlatform.Application.DTOs;
using DataPlatform.Infrastructure.Entities;

namespace DataPlatform.Application.Mappers
{
    public static class LanguageMapper
    {
        public static LanguageDto ToDto(this LanguageEntity entity)
        {
            return new LanguageDto
            {
                Id = entity.Id,
                Culture = entity.Culture,
                DisplayName = entity.DisplayName,
                NativeName = entity.NativeName,
                Direction = entity.Direction,
                Enabled = entity.Enabled,
                Priority = entity.Priority,
                Source = entity.Source,
                CreatedAt = entity.CreatedAt,
                CreatedBy = entity.CreatedBy,
                UpdatedAt = entity.UpdatedAt,
                UpdatedBy = entity.UpdatedBy
            };
        }

        public static LanguageEntity ToEntity(this CreateLanguageDto dto)
        {
            return new LanguageEntity
            {
                Culture = dto.Culture,
                DisplayName = dto.DisplayName,
                NativeName = dto.NativeName,
                Direction = dto.Direction,
                Enabled = dto.Enabled,
                Priority = dto.Priority,
                Source = dto.Source
            };
        }

        public static void UpdateEntity(this UpdateLanguageDto dto, LanguageEntity entity)
        {
            entity.DisplayName = dto.DisplayName;
            entity.NativeName = dto.NativeName;
            entity.Direction = dto.Direction;
            entity.Enabled = dto.Enabled;
            entity.Priority = dto.Priority;
            entity.Source = dto.Source;
        }

        public static IEnumerable<LanguageDto> ToDto(this IEnumerable<LanguageEntity> entities)
        {
            return entities.Select(e => e.ToDto());
        }
    }
}
