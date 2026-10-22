using Adressr.Data.Model;
using Adressr.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Adressr.Data.Repository
{
    public class OpeningRepository : IOpeningRepository
    {
        private readonly AdressrContext _context;

        public OpeningRepository(AdressrContext context)
        {
            _context = context;
        }

        public Task<List<Opening>> GetOpeningsByCompanyIdAsync(int companyId)
        {
            return _context.Openings.AsNoTracking().Where(opening => opening.CompanyID == companyId).OrderByDescending(opening => opening.CreatedDate).ToListAsync();
        }

        public Task<List<Opening>> GetOpeningsByPositionIdAsync(int positionId)
        {
            return _context.Openings.AsNoTracking().Where(opening => opening.PositionID == positionId).OrderByDescending(opening => opening.CreatedDate).ToListAsync();
        }

        public Task<int> GetNumberOfOpeningsByPositionIdAsync(int positionId)
        {
            return _context.Openings.AsNoTracking().Where(opening => opening.PositionID == positionId).SumAsync(opening => opening.NumberOfOpenings);
        }
    }
}
