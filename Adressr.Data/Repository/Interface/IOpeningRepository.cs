using Adressr.Data.Model;

namespace Adressr.Data.Repository.Interface
{
    public interface IOpeningRepository
    {
        Task AddAsync(Opening opening);
        Task<List<Opening>> GetOpeningsByCompanyIdAsync(int companyId);

        Task<int> GetNumberOfOpeningsByPositionIdAsync(int positionId);

        Task<List<Opening>> GetOpeningsByPositionIdAsync(int positionId);

        Task<Dictionary<int, int>> GetNumberOfOpeningsByMultiplePositionIdsAsync(IReadOnlyCollection<int> positionIds);
    }
}
