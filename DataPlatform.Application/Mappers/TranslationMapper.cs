using DataPlatform.Application.DTOs;
using DataPlatform.Infrastructure.Entities;

namespace DataPlatform.Application.Mappers
{
    public static class TranslationMapper
    {
        public static TranslationDto ToDto(this TranslationEntity entity)
        {
            return new TranslationDto
            {
                Id = entity.Id,
                ResourceKey = entity.ResourceKey,
                Culture = entity.Culture,
                Value = entity.Value,
                Context = entity.Context,
                Source = entity.Source,
                Version = entity.Version,
                CreatedAt = entity.CreatedAt,
                CreatedBy = entity.CreatedBy,
                UpdatedAt = entity.UpdatedAt,
                UpdatedBy = entity.UpdatedBy
            };
        }

        public static TranslationEntity ToEntity(this CreateTranslationDto dto)
        {
            return new TranslationEntity
            {
                ResourceKey = dto.ResourceKey,
                Culture = dto.Culture,
                Value = dto.Value,
                Context = dto.Context,
                Source = dto.Source,
                Version = dto.Version
            };
        }

        public static void UpdateEntity(this UpdateTranslationDto dto, TranslationEntity entity)
        {
            entity.Value = dto.Value;
            entity.Context = dto.Context;
            entity.Source = dto.Source;
            entity.Version = dto.Version;
        }

        public static IEnumerable<TranslationDto> ToDto(this IEnumerable<TranslationEntity> entities)
        {
            return entities.Select(e => e.ToDto());
        }
    }
}
