using Adressr.Library.Models;

namespace Adressr.Service.Interface
{
    public interface IExperienceService
    {
        Task<List<ExperienceResponse>> GetExperiencesExportedByProfileIdAsync(int profileId);

        Task<ExperienceResponse> AddExperienceAsync(int userId, CreateExperienceRequest request);
    }
}
