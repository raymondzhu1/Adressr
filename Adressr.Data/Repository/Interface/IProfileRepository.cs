using Adressr.Data.Model;
using Adressr.Data.Repository.Model;

namespace Adressr.Data.Repository.Interface
{
    public interface IProfileRepository
    {
        Task AddProfileAsync(Profile profile);
        Task UpdateProfileAsync(Profile profile);
        Task<Profile?> GetByUserIdAsync(int userId);
        Task<Profile?> GetByUsernameAsync(string username);
        Task<List<UserSearchHit>> SearchUsersAsync(string term, int limit);
    }
}
