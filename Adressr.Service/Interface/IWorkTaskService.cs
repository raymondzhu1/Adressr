using Adressr.Library.Models;

namespace Adressr.Service.Interface
{
    public interface IWorkTaskService
    {
        Task<List<DashboardExpTaskResponse>> GetOpenTasksForUserAsync(int userId);
    }
}
