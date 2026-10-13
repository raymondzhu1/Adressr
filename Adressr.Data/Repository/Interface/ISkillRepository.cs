using Adressr.Data.Model;

namespace Adressr.Data.Repository.Interface
{
    public interface ISkillRepository
    {
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
