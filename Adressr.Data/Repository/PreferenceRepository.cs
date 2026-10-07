using Adressr.Data.Model;
using Adressr.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Adressr.Data.Repository
{
    public class PreferenceRepository : IPreferenceRepository
    {
        private readonly AdressrContext _context;

        public PreferenceRepository(AdressrContext context)
        {
            _context = context;
        }

        public Task AddAsync(Preference preference)
        {
            _context.Preferences.Add(preference);
            return Task.CompletedTask;
        }

        public Task<Preference?> GetByUserIdAsync(int userId)
        {
            return _context.Preferences.SingleOrDefaultAsync(preference => preference.UserID == userId);
        }
    }
}
