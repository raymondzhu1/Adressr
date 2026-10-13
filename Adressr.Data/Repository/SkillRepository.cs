using Adressr.Data.Model;
using Adressr.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Adressr.Data.Repository
{
    public class SkillRepository : ISkillRepository
    {
        private readonly AdressrContext _context;

        public SkillRepository(AdressrContext context)
        {
            _context = context;
        }

        public async Task<ILookup<int, Skill>> GetSkillsByExperienceIdsAsync(IReadOnlyCollection<int> readOnlyExperienceIds)
        {
            //Lookup makes things easier here so that we can accumulate many of the same experience into an index without sorting through it ourselves.
            //ToLookup handles it for us and just returns the Lookup object since multiple entries can be in the same key vs a dictionary.

            var rows = await _context.Skills.AsNoTracking().SelectMany(skill => skill.Experiences.Where(exp => readOnlyExperienceIds.Contains(exp.ExperienceID)),
                                                                      (skill, exp) => new {exp.ExperienceID, Skill=skill}).ToListAsync();

            return rows.ToLookup(row => row.ExperienceID, row => row.Skill);
        }
    }
}
