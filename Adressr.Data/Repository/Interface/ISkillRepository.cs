using Adressr.Data.Model;

namespace Adressr.Data.Repository.Interface
{
    public interface ISkillRepository
    {
        /// <summary>
        /// Adds a skill to the database.
        /// </summary>
        /// <param name="skill">Skill to add to database.</param>
        /// <returns>
        /// Task to await on.
        /// </returns>
        Task AddAsync(Skill skill);

        /// <summary>
        /// Adds a skill to the list of skills in the DbSet, but does not add it to the database immediately.
        /// </summary>
        /// <param name="skill">Skill to add</param>
        /// <returns>
        /// Task to await on.
        /// </returns>
        Task AddLaterAsync(Skill skill);

        /// <summary>
        /// Gets a skill object by the name of the skill.
        /// </summary>
        /// <param name="name">The name of the skill.</param>
        /// <returns>
        /// The skill object that has the matching name given through the parameter. If none found it will be null.
        /// </returns>
        Task<Skill?> GetSkillAsync(string name);

        /// <summary>
        /// Get skills associated with this experience based on the experience ID. This looks through the junction table between Experience and Skill using a skip navigation.
        /// </summary>
        /// <param name="readOnlyExperienceIds">
        /// The readonly collection of experience IDs associated with the experiences you're looking for the skills for.
        /// </param>
        /// <returns>
        /// An ILookup object that relates the experience ID with the associated skills.
        /// ILookup is immutable here and throws no error when using a key that doesn't exist.
        /// </returns>
        Task<ILookup<int, Skill>> GetSkillsByExperienceIdsAsync(IReadOnlyCollection<int> readOnlyExperienceIds);
    }
}
