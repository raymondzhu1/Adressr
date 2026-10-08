using Adressr.Data.Model;
using Adressr.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Adressr.Data.Repository
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly AdressrContext _context;

        public ProfileRepository(AdressrContext context)
        {
            _context = context;
        }

        public Task AddProfileAsync(Profile profile)
        {
            _context.Profiles.Add(profile);
            return Task.CompletedTask;
        }

        public Task UpdateProfileAsync(Profile profile)
        {
            _context.Profiles.Update(profile);
            _context.SaveChangesAsync();
            return Task.CompletedTask;
        }

        public Task<Profile?> GetByUserIdAsync(int userId)
        {
            return _context.Profiles.SingleOrDefaultAsync(profile => profile.UserID == userId);
        }

        public Task<Profile?> GetByUsernameAsync(string username)
        {
            return _context.Profiles.AsNoTracking().Include(profile => profile.User).ThenInclude(user => user.Preference).SingleOrDefaultAsync(profile => profile.User.Username == username);
        }
    }
}
