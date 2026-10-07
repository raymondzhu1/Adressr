using Adressr.Library.Models;

namespace Adressr.Service.Interface
{
    public interface IPreferenceService
    {
        Task<PreferenceDTO?> GetByUserIdAsync(int userId);
        Task UpdatePreferenceAsync(int userId, PreferenceDTO request);
    }
}
