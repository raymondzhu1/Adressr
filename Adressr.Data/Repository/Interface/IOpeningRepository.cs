using Adressr.Data.Model;

namespace Adressr.Data.Repository.Interface
{
    public interface IOpeningRepository
    {
        Task<List<Opening>> GetOpeningsByCompanyIdAsync(int companyId);

        Task<int> GetNumberOfOpeningsByPositionIdAsync(int positionId);

        Task<List<Opening>> GetOpeningsByPositionIdAsync(int positionId);
    }
}
