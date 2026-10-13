using Adressr.Data.Model;
using Adressr.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Adressr.Data.Repository
{
    public class ProfileInterestRepository : IProfileInterestRepository
    {
        private readonly AdressrContext _context;

        public ProfileInterestRepository(AdressrContext context)
        {
            _context = context;
        }

        public Task<List<ProfileInterest>> GetInterestsByProfileIdAsync(int profileId)
        {
            return _context.ProfileInterests.AsNoTracking().Where(interest => interest.ProfileID == profileId).OrderByDescending(interest => interest.CreatedDate).ToListAsync();
        }
    }
}
