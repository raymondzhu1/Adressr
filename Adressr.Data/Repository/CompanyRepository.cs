using Adressr.Data.Model;
using Adressr.Data.Repository.Interface;

namespace Adressr.Data.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly AdressrContext _context;

        public CompanyRepository(AdressrContext context)
        {
            _context = context;
        }

        public async Task<Company> AddAsync(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
            return company;
        }

        public async Task<Company?> GetCompanyByIdAsync(int CompanyID)
        {
            return await _context.Companies.FindAsync(CompanyID);
        }
    }
}
