using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TODO.Core.Dto;
using TODO.Core.Dto.ProjectTask;
using TODO.Core.Entity;

namespace TODO.Infrastructure.Mappers
{
    public static class ProjectTaskMapper
    {
        public static ProjectTask MapCreateDtoToEntity(ProjectTaskCreateDTO dto)
        {
            return new ProjectTask
            {
                Title = dto.Title,
                Description = dto.Description,
                ExpiryDate = dto.ExpiryDate,
                SubTasks = dto.SubTaskDTOs?.Select(SubTaskMapper.MapCreateDtoToEntity).ToList()
                           ?? new List<SubTask>()
            };
        }

        public static ProjectTask MapUpdateDtoToEntity(ProjectTaskUpdateDTO dto)
        {
            return new ProjectTask
            {
                Title = dto.Title,
                Description = dto.Description,
                ExpiryDate = dto.ExpiryDate,
                SubTasks = dto.SubTaskDTOs?.Select(SubTaskMapper.MapDtoToEntity).ToList()
                           ?? new List<SubTask>(),
                TotalProgress = dto.SubTaskDTOs?.Count > 0
                    ? (int?)(dto.SubTaskDTOs.Count(st => st.IsCompleted) / (double)dto.SubTaskDTOs.Count * 100)
                    : 0
            };
        }

        public static ProjectTaskDTO MapEntityToDto(ProjectTask entity)
        {
            return new ProjectTaskDTO
            {
                Title = entity.Title,
                Description = entity.Description,
                ExpiryDate = entity.ExpiryDate,
                TotalProgress = entity.GetTotalProgress(),
                IsActive = entity.IsActive,
                SubTaskDTOs = entity.SubTasks?.Select(SubTaskMapper.MapEntityToDto).ToList()
                              ?? new List<SubTaskDTO>()
            };
        }

        public static IEnumerable<ProjectTaskDTO> MapEntitiesToDtos(IEnumerable<ProjectTask> entities)
        {
            var dtos = new List<ProjectTaskDTO>();

            foreach (var entity in entities)
            {
                var dto = new ProjectTaskDTO
                {

                    Title = entity.Title,
                    Description = entity.Description,
                    ExpiryDate = entity.ExpiryDate,
                    TotalProgress = entity.TotalProgress,
                    IsActive = entity.IsActive,
                    SubTaskDTOs = new List<SubTaskDTO>()
                };

                foreach (var subTask in entity.SubTasks)
                {
                    dto.SubTaskDTOs.Add(new SubTaskDTO
                    {
                        Title = subTask.Title,
                        Description = subTask.Description,
                        ExpiryDate = subTask.ExpiryDate,
                        IsActive = subTask.IsActive,
                        IsCompleted = subTask.IsCompleted
                    });
                }

                dtos.Add(dto);
            }

            return dtos;
        }

    }
}