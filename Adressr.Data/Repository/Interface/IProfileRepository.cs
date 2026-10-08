using Adressr.Data.Model;

namespace Adressr.Data.Repository.Interface
{
    public interface IProfileRepository
    {
        Task AddProfileAsync(Profile profile);
        Task UpdateProfileAsync(Profile profile);
        Task<Profile?> GetByUserIdAsync(int userId);
        Task<Profile?> GetByUsernameAsync(string username);
    }
}
