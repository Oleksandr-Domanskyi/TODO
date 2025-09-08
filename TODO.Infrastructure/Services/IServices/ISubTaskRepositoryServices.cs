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
    public interface ISubTaskRepositoryServices
    {
        Task<Result<SubTaskDTO>> AddSubTask(Guid projectTaskId, SubTaskCreateDTO dto);
        Task<Result> DeleteSubTask(Guid id);
        Task<Result<IEnumerable<SubTaskDTO>>> GetAllSubTasks();
        Task<Result<SubTaskDTO>> GetSubTaskById(Guid id);
        Task<Result<IEnumerable<SubTaskDTO>>> GetIncomingSubTasks(TaskPeriod period);
        Task<Result<SubTaskDTO>> UpdateSubTask(Guid id, SubTaskDTO dto);
        Task<Result<SubTaskDTO>> MarkAsDone(Guid id);

    }
}