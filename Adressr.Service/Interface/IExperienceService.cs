using Adressr.Library.Models;

namespace Adressr.Service.Interface
{
    public interface IExperienceService
    {
        Task<List<ExperienceResponse>> GetExperiencesExportedByProfileIdAsync(int profileId, CancellationToken cancellationToken);

        Task<List<ExperienceResponse>> GetExperiencesForManager(int userId, CancellationToken cancellationToken);

        Task<ExperienceResponse> AddExperienceAsync(int userId, CreateExperienceRequest request);
    }
}
