using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentResults;
using TODO.Core.Dto;
using TODO.Core.Dto.ProjectTask;
using TODO.Core.Enums;
using TODO.Infrastructure.Mappers;
using TODO.Infrastructure.Repositories.IRepositories;
using TODO.Infrastructure.Services.IServices;

namespace TODO.Infrastructure.Services
{
    public class SubTaskRepositoryServices : ISubTaskRepositoryServices
    {
        private readonly ISubTaskRepository _repository;
        private readonly IProjectTaskRepositoryServices _projectTaskRepositoryServices;
        private readonly IProjectTaskRepository _projectTaskRepository;

        public SubTaskRepositoryServices(ISubTaskRepository repository, IProjectTaskRepositoryServices projectTaskRepositoryServices, IProjectTaskRepository projectTaskRepository)
        {
            _projectTaskRepository = projectTaskRepository;
            _projectTaskRepositoryServices = projectTaskRepositoryServices;
            _repository = repository;
        }

        /*  Potential improvements:
            1. Implementing a Unit of Work would allow centralized transaction management
               and error handling across multiple repositories.
            2. Implementing caching (e.g., MemoryCache/Redis) for read operations 
               could improve performance when dealing with frequently accessed subtasks.
        */

        public async Task<Result<SubTaskDTO>> AddSubTask(Guid projectTaskId, SubTaskCreateDTO dto)
            => await Result.Try(async Task<SubTaskDTO> () => await AddSubTaskAsync(projectTaskId, dto));

        public async Task<Result> DeleteSubTask(Guid id)
            => await Result.Try(async Task () => await DeleteSubTaskAsync(id));

        public async Task<Result<IEnumerable<SubTaskDTO>>> GetAllSubTasks()
            => await Result.Try(async Task<IEnumerable<SubTaskDTO>> () => await GetAllSubTasksAsync());

        public async Task<Result<SubTaskDTO>> GetSubTaskById(Guid id)
            => await Result.Try(async Task<SubTaskDTO> () => await GetSubTaskByIdAsync(id));

        public async Task<Result<IEnumerable<SubTaskDTO>>> GetIncomingSubTasks(TaskPeriod period)
            => await Result.Try(async Task<IEnumerable<SubTaskDTO>> () => await GetIncomingAsync(period));

        public async Task<Result<SubTaskDTO>> UpdateSubTask(Guid id, SubTaskDTO dto)
            => await Result.Try(async Task<SubTaskDTO> () => await UpdateSubTaskAsync(id, dto));

        public async Task<Result<SubTaskDTO>> MarkAsDone(Guid id)
            => await Result.Try(async Task<SubTaskDTO> () => await MarkAsDoneAsync(id));


        private async Task<SubTaskDTO> AddSubTaskAsync(Guid projectTaskId, SubTaskCreateDTO dto)
        {
            var entity = SubTaskMapper.MapCreateDtoToEntity(dto);
            entity.ProjectTask_Id = projectTaskId;

            var created = await _repository.AddAsync(entity);

            await _projectTaskRepositoryServices.UpdateTotalProgress(projectTaskId);
            return SubTaskMapper.MapEntityToDto(created);
        }

        private async Task DeleteSubTaskAsync(Guid id)
        {
            var subTask = await _repository.GetByIdAsync(id);

            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
                throw new ArgumentNullException(nameof(id), $"SubTask with Id={id} not found");

            await _projectTaskRepositoryServices.UpdateTotalProgress(subTask!.ProjectTask_Id);
        }

        private async Task<IEnumerable<SubTaskDTO>> GetAllSubTasksAsync()
        {
            var entities = await _repository.GetAllAsync();
            return SubTaskMapper.MapEntitiesToDtos(entities);
        }

        private async Task<SubTaskDTO> GetSubTaskByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), $"SubTask with Id={id} not found");

            return SubTaskMapper.MapEntityToDto(entity);
        }

        private async Task<IEnumerable<SubTaskDTO>> GetIncomingAsync(TaskPeriod period)
        {
            var entities = await _repository.GetIncomingAsync(period);
            return SubTaskMapper.MapEntitiesToDtos(entities);
        }

        private async Task<SubTaskDTO> UpdateSubTaskAsync(Guid id, SubTaskDTO dto)
        {
            var entity = SubTaskMapper.MapDtoToEntity(dto);
            entity.Id = id;

            var updated = await _repository.UpdateAsync(entity);
            if (updated == null)
                throw new ArgumentNullException(nameof(entity), $"SubTask with Id={id} not found");

            return SubTaskMapper.MapEntityToDto(updated);
        }

        private async Task<SubTaskDTO> MarkAsDoneAsync(Guid id)
        {

            var success = await _repository.MarkAsDoneAsync(id);
            if (!success)
                throw new ArgumentNullException(nameof(id), $"SubTask with Id={id} not found");


            var updatedSubTask = await _repository.GetByIdAsync(id);
            if (updatedSubTask == null)
                throw new ArgumentNullException(nameof(id), $"SubTask with Id={id} not found after update");

            await _projectTaskRepositoryServices.UpdateTotalProgress(updatedSubTask.ProjectTask_Id);


            return SubTaskMapper.MapEntityToDto(updatedSubTask);
        }

    }
}
