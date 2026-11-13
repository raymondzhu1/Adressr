using Adressr.Data.Model;
using Adressr.Library.Models;

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

        /// <summary>
        /// Gets the list of experiences associated with the user of this user ID.
        /// </summary>
        /// <param name="userId">The User ID to search experiences for.</param>
        /// <returns>
        /// List of current experiences associated with a specific user ID.
        /// </returns>
        Task<List<Experience>> GetCurrentExperiencseByUserIdAsync(int userId);

        /// <summary>
        /// Gets all of the experiences of the user given.
        /// </summary>
        /// <param name="userId">The User ID to search experiences for.</param>
        /// <returns>
        /// List of experiences associated with this specific user ID.
        /// </returns>
        Task<List<Experience>> GetAllExperienceByUserIdAsync(int userId);

        /// <summary>
        /// Adds the given Experience to the DB.
        /// </summary>
        /// <param name="experience">The experience to save to database.</param>
        /// <returns>
        /// Task to await on.
        /// </returns>
        Task AddAsync(Experience experience);

        /// <summary>
        /// Updates the Experience's basic properties: CompanyName, RoleTitle, Description, StartDate, EndDate(if available).
        /// </summary>
        /// <param name="experience">
        /// The experience used to update the experience in the database.
        /// </param>
        /// <returns>
        /// The updated experience entry that is to be updated in the database. Returns null if no corresponding experience exists.
        /// </returns>
        Task<Experience?> UpdateGeneralAsync(Experience experience);

        /// <summary>
        /// Updates the Experience's publicity on your profile.
        /// </summary>
        /// <param name="request">
        /// Custom DTO for an experience's publicity request. Contains the ID of the experience to change and whether or not to export to the associated user's profile.
        /// </param>
        /// <returns>
        /// The updated experience entry to be updated in the database. Returns null if no corresponding experience exists.
        /// </returns>
        Task<Experience?> UpdatePublicityAsync(ExperiencePublicityRequest request);
    }
}
