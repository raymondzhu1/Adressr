using Adressr.Data.Model;

namespace Adressr.Data.Repository.Interface
{
    public interface ICompanyRepository
    {
        Task<Company> AddAsync(Company company);
        Task<Company?> GetCompanyByIdAsync(int CompanyID);
    }
}
