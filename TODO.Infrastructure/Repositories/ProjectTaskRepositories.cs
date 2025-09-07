using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TODO.Core.Entity;
using TODO.Infrastructure.DataBase;
using TODO.Infrastructure.Repositories.IRepositories;

namespace TODO.Infrastructur.Repositories
{
    public class ProjectTaskRepository : IProjectTaskRepository
    {
        private readonly TodoDbContext _dbContext;

        public ProjectTaskRepository(TodoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /*
            We could also add filtering and modify the repository results based on specific rules.
            However, since filtering was not explicitly mentioned in the requirements, it has been omitted for now.
        */
        public async Task<IEnumerable<ProjectTask>> GetAllAsync() =>
            await _dbContext.ProjectTasks
                .Include(p => p.SubTasks)
                .ToListAsync();
        public async Task<ProjectTask?> GetByIdAsync(Guid id) =>
            await _dbContext.ProjectTasks
                .Include(p => p.SubTasks)
                .FirstOrDefaultAsync(p => p.Id == id);

        public async Task<IEnumerable<ProjectTask>> GetIncomingAsync(DateTime? from = null, DateTime? to = null)
        {
            from ??= DateTime.UtcNow.Date;
            to ??= from.Value.AddDays(5);

            return await _dbContext.ProjectTasks
                .Include(p => p.SubTasks)
                .Where(p => p.ExpiryDate >= from && p.ExpiryDate <= to)
                .ToListAsync();
        }

        public async Task<ProjectTask> CreateAsync(ProjectTask projectTask)
        {
            await _dbContext.ProjectTasks.AddAsync(projectTask);
            await _dbContext.SaveChangesAsync();
            return projectTask;
        }

        public async Task<ProjectTask?> UpdateAsync(ProjectTask projectTask)
        {
            var exists = await _dbContext.ProjectTasks.AnyAsync(p => p.Id == projectTask.Id);
            if (!exists) return null;

            _dbContext.ProjectTasks.Update(projectTask);
            await _dbContext.SaveChangesAsync();
            return projectTask;
        }

        public async Task<bool> SetPercentCompleteAsync(Guid id)
        {
            var task = await _dbContext.ProjectTasks.FindAsync(id);
            if (task == null) return false;

            task.TotalProgress = 100;
            _dbContext.ProjectTasks.Update(task);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MarkDoneAsync(Guid id)
        {
            var task = await _dbContext.ProjectTasks.FindAsync(id);
            if (task == null) return false;

            task.TotalProgress = 100;
            _dbContext.ProjectTasks.Update(task);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var task = await _dbContext.ProjectTasks.FindAsync(id);
            if (task == null) return false;

            _dbContext.ProjectTasks.Remove(task);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
