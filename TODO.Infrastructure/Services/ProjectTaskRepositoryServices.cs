using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using TODO.Core.Dto;
using TODO.Core.Dto.ProjectTask;
using TODO.Infrastructure.Mappers;
using TODO.Infrastructure.Repositories.IRepositories;
using TODO.Infrastructure.Services.IServices;

namespace TODO.Infrastructure.Services
{
    public class ProjectTaskRepositoryServices : IProjectTaskRepositoryServices
    {
        private readonly IProjectTaskRepository _repository;

        public ProjectTaskRepositoryServices(IProjectTaskRepository repository)
        {
            _repository = repository;
        }

        /*  Potential improvements:
            1. Implementing a Unit of Work would allow centralized transaction management
            and error handling across multiple repositories.
            2. Implementing IDisposable would enable proper resource cleanup (e.g., DbContext),
            especially if this class manages multiple services or repositories.
        */

        public async Task<Result<ProjectTaskDTO>> AddProjectTask(ProjectTaskCreateDTO dto)
            => await Result.Try(async Task<ProjectTaskDTO> () => await AddProjectTaskAsync(dto));
        public async Task<Result> DeleteProjectTask(Guid id)
            => await Result.Try(async Task () => await DeleteProjectTaskAsync(id));
        public async Task<Result<IEnumerable<ProjectTaskDTO>>> GetAllProjectTasks()
            => await Result.Try(async Task<IEnumerable<ProjectTaskDTO>> () => await GetAllProjectTasksAsync());
        public async Task<Result<ProjectTaskDTO>> GetProjectTaskById(Guid id)
            => await Result.Try(async Task<ProjectTaskDTO> () => await GetProjectTaskByIdAsync(id));
        public async Task<Result<IEnumerable<ProjectTaskDTO>>> GetIncomingProjectTaskAsync()
            => await Result.Try(async Task<IEnumerable<ProjectTaskDTO>> () => await GetIncomingAsync());
        public async Task<Result<ProjectTaskDTO>> UpdateProjectTask(Guid id, ProjectTaskUpdateDTO dto)
            => await Result.Try(async Task<ProjectTaskDTO> () => await UpdateProjectTaskAsync(id, dto));
        public async Task<Result<ProjectTaskDTO>> SetAsComplatePercentComplete(Guid id)
            => await Result.Try(async Task<ProjectTaskDTO> () => await SetPercentCompleteAsync(id));
        public async Task<Result<ProjectTaskDTO>> MarkDone(Guid id)
            => await Result.Try(async Task<ProjectTaskDTO> () => await MarkDoneAsync(id));



        private async Task<ProjectTaskDTO> AddProjectTaskAsync(ProjectTaskCreateDTO dto)
        {
            var entity = ProjectTaskMapper.MapCreateDtoToEntity(dto);
            var created = await _repository.CreateAsync(entity);
            return ProjectTaskMapper.MapEntityToDto(created);
        }

        private async Task DeleteProjectTaskAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        private async Task<IEnumerable<ProjectTaskDTO>> GetAllProjectTasksAsync()
        {
            var entities = await _repository.GetAllAsync();
            return ProjectTaskMapper.MapEntitiesToDtos(entities);
        }

        private async Task<ProjectTaskDTO> GetProjectTaskByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), $"ProjectTask with Id={id} not found");

            return ProjectTaskMapper.MapEntityToDto(entity);
        }
        private async Task<IEnumerable<ProjectTaskDTO>> GetIncomingAsync()
        {
            var entities = await _repository.GetIncomingAsync();
            return ProjectTaskMapper.MapEntitiesToDtos(entities);
        }

        private async Task<ProjectTaskDTO> UpdateProjectTaskAsync(Guid id, ProjectTaskUpdateDTO dto)
        {
            var entity = ProjectTaskMapper.MapUpdateDtoToEntity(dto);
            entity.Id = id;
            var updated = await _repository.UpdateAsync(entity);
            return ProjectTaskMapper.MapEntityToDto(updated!);
        }

        private async Task<ProjectTaskDTO> SetPercentCompleteAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), $"ProjectTask with Id={id} not found");

            entity.TotalProgress = 100;
            var updated = await _repository.UpdateAsync(entity);
            return ProjectTaskMapper.MapEntityToDto(updated!);
        }

        private async Task<ProjectTaskDTO> MarkDoneAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), $"ProjectTask with Id={id} not found");

            entity.TotalProgress = entity.GetTotalProgress();
            var updated = await _repository.UpdateAsync(entity);
            return ProjectTaskMapper.MapEntityToDto(updated!);
        }
    }
}