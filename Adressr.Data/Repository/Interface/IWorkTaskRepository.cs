using Adressr.Data.Model;

namespace Adressr.Data.Repository.Interface
{
    public interface IWorkTaskRepository
    {
        Task AddAsync(WorkTask task);
        Task AddLaterAsync(WorkTask task);
        Task<List<WorkTask>> GetTaskByExperienceIdsAsync(IReadOnlyCollection<int> readOnlyExperienceIds);
    }
}
