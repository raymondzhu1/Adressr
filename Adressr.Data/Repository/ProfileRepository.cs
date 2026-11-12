using Adressr.Data.Model;
using Adressr.Data.Repository.Interface;
using Adressr.Data.Repository.Model;
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

        public async Task<List<UserSearchHit>> SearchUsersAsync(string term, int limit)
        {
            //By default SQL Server is case-insensitive. So because we're in EF, these will turn into queries and casing won't matter unless collation is changed. If collation is changed, such that you have case sensitivity everywhere you need to specify StringComparison.OrdinalIgnoreCase in .StartsWith and .Contains.
            IQueryable<Profile> visibleProfiles = _context.Profiles.AsNoTracking().Where(profile => profile.User.Preference == null || !profile.User.Preference.ProfilePrivate);

            List<UserSearchHit> startsWithHits = await visibleProfiles.Where(profile => profile.Name.StartsWith(term)).OrderBy(profile => profile.Name).Select(profile => new UserSearchHit(profile.User.Username, profile.Name, profile.ProfilePicLocation)).Take(limit).ToListAsync();
            if(startsWithHits.Count >= limit)
            {
                return startsWithHits;
            }

            List<UserSearchHit> containsHits = await visibleProfiles.Where(profile => profile.Name.Contains(term) && !profile.Name.StartsWith(term)).OrderBy(profile => profile.Name).Select(profile => new UserSearchHit(profile.User.Username, profile.Name, profile.ProfilePicLocation)).Take(limit - startsWithHits.Count).ToListAsync();

            startsWithHits.AddRange(containsHits);
            return startsWithHits;
            //return _context.Profiles.AsNoTracking().Where(profile => profile.Name.Contains(term)).Where(profile => profile.User.Preference == null || !profile.User.Preference.ProfilePrivate).OrderByDescending(profile => profile.Name.StartsWith(term)).ThenBy(profile => profile.Name).Select(profile => new UserSearchHit(profile.User.Username, profile.Name, profile.ProfilePicLocation)).Take(limit).ToListAsync();
        }
    }
}
