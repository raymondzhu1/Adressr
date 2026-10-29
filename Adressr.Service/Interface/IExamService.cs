using Adressr.Library.Models;

namespace Adressr.Service.Interface
{
    public interface IExamService
    {
        /// <summary>
        /// Get all the exams, or more like a lightweight DTO of it, that this user is invited to.
        /// </summary>
        /// <param name="userId">User ID of the user that is checking what exams they are invited to.</param>
        /// <returns>
        /// A list of simplified exam objects, without any questions and other metadata, that the user specified is invited to. Note this does not return any public exams, as those are open to everyone.
        /// </returns>
        Task<List<DashboardExamResponse>> GetExamsInvitedToAsync(int userId);
    }
}
