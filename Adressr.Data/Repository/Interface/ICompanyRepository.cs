using Adressr.Data.Model;

namespace Adressr.Data.Repository.Interface
{
    public interface ICompanyRepository
    {
        Task AddAsync(Company company);
        Task<Company?> GetCompanyByIdAsync(int CompanyID);
        Task<List<Company>> GetByCreatorIdAsync(int creatorId);
    }
}
