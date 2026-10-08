using Adressr.Data.Model;
using Adressr.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Adressr.Data.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly AdressrContext _context;

        public CompanyRepository(AdressrContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
            return;
        }

        public Task<Company?> GetCompanyByIdAsync(int CompanyID)
        {
            return _context.Companies.SingleOrDefaultAsync(company => company.CompanyID == CompanyID);
        }

        public Task<List<Company>> GetByCreatorIdAsync(int creatorId)
        {
            return _context.Companies.AsNoTracking().Where(company => company.CreatorID == creatorId).OrderBy(company => company.Name).ToListAsync();
        }
    }
}
