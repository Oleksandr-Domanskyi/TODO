using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TODO.Core.Dto;
using TODO.Core.Dto.ProjectTask;
using TODO.Core.Entity;

namespace TODO.Infrastructure.Mappers
{
    public static class SubTaskMapper
    {
        public static SubTask MapCreateDtoToEntity(SubTaskCreateDTO dto)
        {
            return new SubTask
            {
                Title = dto.Title,
                Description = dto.Description,
                ExpiryDate = dto.ExpiryDate,
                IsActive = true,
                IsCompleted = false
            };
        }

        public static SubTask MapDtoToEntity(SubTaskDTO dto)
        {
            return new SubTask
            {
                Title = dto.Title,
                Description = dto.Description,
                ExpiryDate = dto.ExpiryDate,
                IsActive = dto.IsActive,
                IsCompleted = dto.IsCompleted
            };
        }

        public static SubTaskDTO MapEntityToDto(SubTask entity)
        {
            return new SubTaskDTO
            {
                Title = entity.Title,
                Description = entity.Description,
                ExpiryDate = entity.ExpiryDate,
                IsActive = entity.IsActive,
                IsCompleted = entity.IsCompleted
            };
        }

        public static IEnumerable<SubTaskDTO> MapEntitiesToDtos(IEnumerable<SubTask> entities)
            => entities.Select(MapEntityToDto);

        public static IEnumerable<SubTask> MapDtosToEntities(IEnumerable<SubTaskDTO> dtos)
            => dtos.Select(MapDtoToEntity);
    }
}