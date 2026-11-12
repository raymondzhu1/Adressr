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

        public async Task AddAsync(Skill skill)
        {
            //Skill name is unique here, so if one already exists make sure to throw already exists custom error or something later in service. Use GetSkillAsync to check before using this method, defer to pointing to existing skill if needed rather than add.
            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();
            return;
        }

        public Task AddLaterAsync(Skill skill)
        {
            //If skill name exists when using this as part of another service, and not its own thing, then dont throw an error and just refer it to the existing skill with junction table.
            _context.Skills.Add(skill);
            return Task.CompletedTask;
        }

        public Task<Skill?> GetSkillAsync(string name)
        {
            return _context.Skills.SingleOrDefaultAsync(skill => skill.Name == name);
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
