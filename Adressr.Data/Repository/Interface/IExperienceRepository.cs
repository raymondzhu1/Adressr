using Adressr.Data.Model;

namespace Adressr.Data.Repository.Interface
{
    public interface IExperienceRepository
    {
        /// <summary>
        /// Receives a profile id for some user's profile that then goes and checks the experience table for any
        /// experiences that are flagged as being exported from the career manager to being publically available
        /// in their respective profile.
        /// </summary>
        /// <param name="profileId">
        /// A user's profile ID for their publically displayed profile.
        /// </param>
        /// <returns>
        /// A list of experiences corresponding to the profile id given.
        /// </returns>
        Task<List<Experience>> GetExportedByProfileIdAsync(int profileId);
    }
}
