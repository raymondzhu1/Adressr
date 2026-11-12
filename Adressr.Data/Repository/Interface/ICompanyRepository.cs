using Adressr.Data.Model;
using Adressr.Data.Repository.Model;

namespace Adressr.Data.Repository.Interface
{
    public interface ICompanyRepository
    {
        Task AddAsync(Company company);
        Task<Company?> GetCompanyByIdAsync(int CompanyID);
        Task<List<Company>> GetByCreatorIdAsync(int creatorId);
        Task<int> GetCreatorIdAsync(int companyId);
        Task<List<CompanySearchHit>> SearchCompanyAsync(string term, int limit);
    }
}
