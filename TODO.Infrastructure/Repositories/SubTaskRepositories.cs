using Microsoft.EntityFrameworkCore;
using TODO.Core.Entity;
using TODO.Core.Enums;
using TODO.Infrastructure.DataBase;
using TODO.Infrastructure.Repositories.IRepositories;

namespace TODO.Infrastructure.Repositories
{
    public class SubTaskRepository : ISubTaskRepository
    {
        private readonly TodoDbContext _DbContext;

        public SubTaskRepository(TodoDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        public async Task<IEnumerable<SubTask>> GetAllAsync()
        {
            return await _DbContext.TodoModSubTaskModels.AsNoTracking().ToListAsync();
        }

        public async Task<SubTask?> GetByIdAsync(Guid id)
        {
            return await _DbContext.TodoModSubTaskModels.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<SubTask>> GetIncomingAsync(TaskPeriod period)
        {
            var today = DateTime.UtcNow.Date;
            IQueryable<SubTask> query = _DbContext.TodoModSubTaskModels.AsNoTracking();

            switch (period)
            {
                case TaskPeriod.Today:
                    query = query.Where(s => s.ExpiryDate.Date == today);
                    break;
                case TaskPeriod.NextDay:
                    query = query.Where(s => s.ExpiryDate.Date == today.AddDays(1));
                    break;
                case TaskPeriod.CurrentWeek:
                    var startOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
                    var endOfWeek = startOfWeek.AddDays(6);
                    query = query.Where(s => s.ExpiryDate.Date >= startOfWeek && s.ExpiryDate.Date <= endOfWeek);
                    break;
            }

            return await query.ToListAsync();
        }

        public async Task<SubTask> AddAsync(SubTask entity)
        {
            await _DbContext.TodoModSubTaskModels.AddAsync(entity);
            await _DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<SubTask> UpdateAsync(SubTask entity)
        {
            _DbContext.TodoModSubTaskModels.Update(entity);
            await _DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> MarkAsDoneAsync(Guid id)
        {
            var subTask = await _DbContext.TodoModSubTaskModels.FirstOrDefaultAsync(s => s.Id == id);
            if (subTask == null) return false;

            subTask.IsCompleted = true;
            _DbContext.TodoModSubTaskModels.Update(subTask);
            await _DbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var subTask = await _DbContext.TodoModSubTaskModels.FirstOrDefaultAsync(s => s.Id == id);
            if (subTask == null) return false;

            _DbContext.TodoModSubTaskModels.Remove(subTask);
            await _DbContext.SaveChangesAsync();
            return true;
        }
    }
}
