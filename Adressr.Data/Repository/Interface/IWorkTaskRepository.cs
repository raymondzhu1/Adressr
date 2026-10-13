using Adressr.Data.Model;

namespace Adressr.Data.Repository.Interface
{
    public interface IWorkTaskRepository
    {
        Task<List<WorkTask>> GetTaskByExperienceIdsAsync(IReadOnlyCollection<int> readOnlyExperienceIds);
    }
}
