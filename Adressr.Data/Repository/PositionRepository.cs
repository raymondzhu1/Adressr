using Adressr.Data.Model;
using Adressr.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Adressr.Data.Repository
{
    public class PositionRepository : IPositionRepository
    {
        private readonly AdressrContext _context;

        public PositionRepository(AdressrContext context)
        {
            _context = context;
        }

        public async Task AddPositionToCompanyAsync(Position position)
        {
            _context.Positions.Add(position);
            await _context.SaveChangesAsync();
            return;
        }

        public Task<Position?> GetPositionByPositionIdAsync(int positionId)
        {
            return _context.Positions.AsNoTracking().SingleOrDefaultAsync(pos => pos.PositionID == positionId);
        }

        public Task<List<Position>> GetPositionsByCompanyIdAsync(int companyId)
        {
            return _context.Positions.AsNoTracking().Where(position => position.CompanyID == companyId).OrderBy(position => position.Title).ToListAsync();
        }

        public async Task<int> GetNumberOfEmployeesForPositionByPositionIdAsync(int positionId)
        {
            int totalRegisteredEmployees = await _context.WorkersToPosition.CountAsync(wp => wp.PositionID == positionId);  //await _context.Users.CountAsync(user => user.Positions.Any(position => position.PositionID == positionId));
            int totalUnregisteredEmployees = await _context.PlainTextEmployees.CountAsync(emp => emp.Positions.Any(position => position.PositionID == positionId));
            return totalRegisteredEmployees + totalUnregisteredEmployees;
        }

        public Task<List<User>> GetRegisterEmployeesByPositionIdAsync(int positionId)
        {
            return _context.Users.AsNoTracking().Where(user => user.Positions.Any(wp => wp.PositionID == positionId)).ToListAsync();
        }

        public Task<List<PlainTextEmployee>> GetPlainTextEmployeesByPositionIdAsync(int positionId)
        {
            return _context.PlainTextEmployees.AsNoTracking().Where(plain => plain.Positions.Any(position => position.PositionID == positionId)).ToListAsync();
        }
    }
}
