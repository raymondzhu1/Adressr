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

        public Task<List<WorkTask>> GetTaskByExperienceIdsAsync(IReadOnlyCollection<int> readOnlyExperienceIds)
        {
            return _context.Tasks.AsNoTracking().Where(task => readOnlyExperienceIds.Contains(task.ExperienceID)).ToListAsync();
        }
    }
}
