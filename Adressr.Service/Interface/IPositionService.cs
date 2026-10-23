using Adressr.Library.Models;

namespace Adressr.Service.Interface
{
    public interface IPositionService
    {
        Task<List<PositionResponse>?> GetPositionsByCompanyIdAsync(int requesterId, int companyId);

        Task<PositionResponse?> CreatePositionByCompanyIdAsync(int requesterId,int companyId, PositionRequest positionRequest);

        Task<PositionResponse?> GetPositionByPositionIdAsync(int requesterId, int companyId, int positionId);
    }
}
