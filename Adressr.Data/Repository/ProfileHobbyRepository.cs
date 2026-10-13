using Adressr.Data.Model;
using Adressr.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Adressr.Data.Repository
{
    public class ProfileHobbyRepository : IProfileHobbyRepository
    {
        private readonly AdressrContext _context;

        public ProfileHobbyRepository(AdressrContext context)
        {
            _context = context;
        }

        public Task<List<ProfileHobby>> GetHobbiesByProfileIdAsync(int profileId)
        {
            return _context.ProfileHobbies.AsNoTracking().Where(hobby => hobby.ProfileID == profileId).OrderByDescending(hobby => hobby.CreatedDate).ToListAsync();
        }
    }
}
