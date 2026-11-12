using Adressr.Data.Model;
using Adressr.Data.Repository.Interface;
using Adressr.Data.Repository.Model;
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

        public Task<int> GetCreatorIdAsync(int companyId)
        {
            return _context.Companies.AsNoTracking().Where(company => company.CompanyID == companyId).Select(company => company.CreatorID).FirstOrDefaultAsync();
        }

        public async Task<List<CompanySearchHit>> SearchCompanyAsync(string term, int limit)
        {
            IQueryable<Company> companies = _context.Companies.AsNoTracking();

            List<CompanySearchHit> startsWithHits = await companies.Where(company => company.Name.StartsWith(term)).OrderBy(company => company.Name).Select(company => new CompanySearchHit(company.CompanyID, company.Name, company.CompanyIconImageLocation)).Take(limit).ToListAsync();
            if(startsWithHits.Count >= limit)
            {
                return startsWithHits;
            }

            List<CompanySearchHit> containsHits = await companies.Where(company => company.Name.Contains(term) && !company.Name.StartsWith(term)).OrderBy(company => company.Name).Select(company => new CompanySearchHit(company.CompanyID, company.Name, company.CompanyIconImageLocation)).Take(limit - startsWithHits.Count).ToListAsync();

            startsWithHits.AddRange(containsHits);
            return startsWithHits;
            //return _context.Companies.AsNoTracking().Where(company => company.Name.Contains(term)).OrderByDescending(company => company.Name.StartsWith(term)).ThenBy(company => company.Name).Select(company => new CompanySearchHit(company.CompanyID, company.Name, company.CompanyIconImageLocation)).Take(limit).ToListAsync();
        }
    }
}
