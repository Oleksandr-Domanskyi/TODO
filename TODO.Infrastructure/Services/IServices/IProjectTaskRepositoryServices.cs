using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using TODO.Core.Dto;
using TODO.Core.Dto.ProjectTask;
using TODO.Core.Enums;

namespace TODO.Infrastructure.Services.IServices
{
    public interface IProjectTaskRepositoryServices
    {
        Task<Result<ProjectTaskDTO>> AddProjectTask(ProjectTaskCreateDTO dto);
        Task<Result> DeleteProjectTask(Guid id);
        Task<Result<IEnumerable<ProjectTaskDTO>>> GetAllProjectTasks();
        Task<Result<ProjectTaskDTO>> GetProjectTaskById(Guid id);
        Task<Result<IEnumerable<ProjectTaskDTO>>> GetIncomingProjectTaskAsync(TaskPeriod period);
        Task<Result<ProjectTaskDTO>> UpdateProjectTask(Guid id, ProjectTaskUpdateDTO dto);
        Task<Result<ProjectTaskDTO>> SetAsComplatePercentComplete(Guid id);
        Task<Result> UpdateTotalProgress(Guid id);

    }

}