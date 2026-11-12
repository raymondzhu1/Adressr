using Adressr.Data.Model;
using Adressr.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Adressr.Data.Repository
{
    public class WorkTaskRepository : IWorkTaskRepository
    {
        private readonly AdressrContext _context;

        public WorkTaskRepository(AdressrContext context)
        {
            _context = context;
        }

        public async Task AddAsync(WorkTask task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return;
        }

        public Task AddLaterAsync(WorkTask task)
        {
            _context.Tasks.Add(task);
            return Task.CompletedTask;
        }

        public Task<List<WorkTask>> GetTaskByExperienceIdsAsync(IReadOnlyCollection<int> readOnlyExperienceIds)
        {
            return _context.Tasks.AsNoTracking().Where(task => readOnlyExperienceIds.Contains(task.ExperienceID)).ToListAsync();
        }
    }
}
