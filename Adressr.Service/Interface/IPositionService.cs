using Adressr.Library.Models;

namespace Adressr.Service.Interface
{
    public interface IPositionService
    {
        Task<List<PositionResponse>> GetPositionsByCompanyIdAsync(int companyId);

        Task<PositionResponse> CreatePositionByCompanyIdAsync(int companyId, PositionRequest positionRequest);

        Task<PositionResponse?> GetPositionByPositionIdAsync(int companyId, int positionId);
    }
}
