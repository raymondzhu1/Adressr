using Adressr.Data.Model;

namespace Adressr.Data.Repository.Interface
{
    public interface IPreferenceRepository
    {
        Task AddAsync(Preference preference);
    }
}
