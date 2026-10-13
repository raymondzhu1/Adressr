using Adressr.Data.Model;

namespace Adressr.Data.Repository.Interface
{
    public interface IProfileInterestRepository
    {
        Task<List<ProfileInterest>> GetInterestsByProfileIdAsync(int profileId);
    }
}
