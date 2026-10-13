using Adressr.Data.Model;
using Adressr.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Adressr.Data.Repository
{
    public class ExperienceRepository : IExperienceRepository
    {
        private readonly AdressrContext _context;

        public ExperienceRepository(AdressrContext context)
        {
            _context = context;
        }

        public Task<List<Experience>> GetExportedByProfileIdAsync(int profileId)
        {
            return _context.Experiences.AsNoTracking().Where(exp => exp.ProfileID == profileId && exp.IsExported).OrderByDescending(exp => exp.StartDate).ToListAsync();
        }
    }
}
