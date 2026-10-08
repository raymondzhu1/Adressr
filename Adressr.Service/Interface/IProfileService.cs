using Adressr.Library.Models;

namespace Adressr.Service.Interface
{
    public interface IProfileService
    {
        Task<ProfileResponse?> GetByUsernameAsync(string username, int? viewerUserId);
        Task UpdateProfileAsync(int userId, ProfileRequest request);
    }
}
