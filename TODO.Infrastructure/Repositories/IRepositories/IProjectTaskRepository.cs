using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TODO.Core.Entity;
using TODO.Core.Enums;

namespace TODO.Infrastructure.Repositories.IRepositories
{
    public interface IProjectTaskRepository
    {
        Task<IEnumerable<ProjectTask>> GetAllAsync();
        Task<ProjectTask?> GetByIdAsync(Guid id);
        Task<IEnumerable<ProjectTask>> GetIncomingAsync(TaskPeriod period);
        Task<ProjectTask> CreateAsync(ProjectTask projectTask);
        Task<ProjectTask?> UpdateAsync(ProjectTask projectTask);
        Task<bool> SetPercentCompleteAsync(Guid id);
        Task<bool> DeleteAsync(Guid id);
    }
}