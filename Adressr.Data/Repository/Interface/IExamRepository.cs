using Adressr.Data.Model;

namespace Adressr.Data.Repository.Interface
{
    public interface IExamRepository
    {
        /// <summary>
        /// Gets all the private exams that the specific user is invited to.
        /// </summary>
        /// <param name="userId">The user ID for the specified user.</param>
        /// <returns>
        /// A list of private exams where the userId is within the known list of participants.
        /// </returns>
        Task<List<Exam>> GetExamsInvitedToByUserIdAsync(int userId);
    }
}
