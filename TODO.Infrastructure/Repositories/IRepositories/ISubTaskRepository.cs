using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TODO.Core.Entity;
using TODO.Core.Enums;

namespace TODO.Infrastructure.Repositories.IRepositories
{
    public interface ISubTaskRepository
    {
        Task<IEnumerable<SubTask>> GetAllAsync();
        Task<SubTask?> GetByIdAsync(Guid id);
        Task<IEnumerable<SubTask>> GetIncomingAsync(TaskPeriod period);
        Task<SubTask> AddAsync(SubTask entity);
        Task<SubTask> UpdateAsync(SubTask entity);
        Task<bool> MarkAsDoneAsync(Guid id);
        Task<bool> DeleteAsync(Guid id);
    }
}