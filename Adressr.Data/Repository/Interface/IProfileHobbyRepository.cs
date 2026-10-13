using Adressr.Data.Model;

namespace Adressr.Data.Repository.Interface
{
    public interface IProfileHobbyRepository
    {
        Task<List<ProfileHobby>> GetHobbiesByProfileIdAsync(int profileId);
    }
}
