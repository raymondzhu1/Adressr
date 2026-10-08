using Adressr.Library.Models;

namespace Adressr.Service.Interface
{
    public interface ICompanyService
    {
        Task<CompanyResponse> CreateAsync(int creatorUserId, CompanyRequest companyRequest);
        Task<CompanyResponse?> GetByIdAsync(int companyId);
        Task<List<CompanyResponse>> GetByCreatorIdAsync(int creatorUserId);
    }
}
