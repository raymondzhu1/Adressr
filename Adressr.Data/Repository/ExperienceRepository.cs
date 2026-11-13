using Adressr.Data.Model;
using Adressr.Data.Repository.Interface;
using Adressr.Library.Models;
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

        public async Task AddAsync(Experience experience)
        {
            _context.Experiences.Add(experience);
            await _context.SaveChangesAsync();
            return;
        }

        public Task<List<Experience>> GetExportedByProfileIdAsync(int profileId)
        {
            return _context.Experiences.AsNoTracking().Where(exp => exp.ProfileID == profileId && exp.IsExported).OrderByDescending(exp => exp.StartDate).ToListAsync();
        }

        public Task<List<Experience>> GetCurrentExperiencseByUserIdAsync(int userId)
        {
            return _context.Experiences.AsNoTracking().Where(exp => exp.Profile.UserID == userId && exp.EndDate == null).OrderByDescending(exp => exp.StartDate).ToListAsync();
        }

        public Task<List<Experience>> GetAllExperienceByUserIdAsync(int userId)
        {
            //Could've probably done some kind of factory design pattern with GetCurrentExperiences, but I'll just throw this in for now.
            return _context.Experiences.AsNoTracking().Where(exp => exp.Profile.UserID == userId).OrderByDescending(exp => exp.StartDate).ToListAsync();
        }

        public async Task<Experience?> UpdateGeneralAsync(Experience experience)
        {
            //This is but one part of the update process, so no SaveChangesAsync() at the end here, otherwise this whole process is not atomic. Still need to update work tasks and maybe skills.
            Experience? queriedResult = await _context.Experiences.FirstOrDefaultAsync(exp => exp.ExperienceID == experience.ExperienceID);
            if(queriedResult == null)
            {
                return null;
            }

            queriedResult.CompanyName = experience.CompanyName;
            queriedResult.RoleTitle = experience.RoleTitle;
            queriedResult.Description = experience.Description;
            queriedResult.StartDate = experience.StartDate;
            queriedResult.EndDate = experience.EndDate;

            return queriedResult;  
        }

        public async Task<Experience?> UpdatePublicityAsync(ExperiencePublicityRequest request)
        {
            Experience? experience = await _context.Experiences.FirstOrDefaultAsync(exp => exp.ExperienceID == request.ExperienceID);
            if (experience == null)
            {
                return null;
            }

            experience.IsExported = request.IsExported;

            await _context.SaveChangesAsync();
            return experience;
        }
    }
}
