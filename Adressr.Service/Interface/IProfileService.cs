using Adressr.Library.Models;

namespace Adressr.Service.Interface
{
    public interface IProfileService
    {
        Task<ProfileResponse?> GetByUsernameAsync(string username, int? viewerUserId, CancellationToken cancellationToken);
        Task UpdateProfileAsync(int userId, ProfileRequest request);
    }
}
