using Adressr.Data.Model;
using Adressr.Data.Repository.Interface;
using Adressr.Library.Models;
using Adressr.Service.Interface;

namespace Adressr.Service
{
    public class ExamService : IExamService
    {
        private readonly IExamRepository _examRepository;

        public ExamService(IExamRepository examRepository)
        {
            _examRepository = examRepository;
        }

        public async Task<List<DashboardExamResponse>> GetExamsInvitedToAsync(int userId)
        {
            List<Exam> exams = await _examRepository.GetExamsInvitedToByUserIdAsync(userId);

            return exams.Select(exam => new DashboardExamResponse
            {
                ExamID = exam.ExamID,
                Title = exam.Title,
                TimeLimitMinutes = (int)exam.TimeLimit.TotalMinutes
            }).ToList();
        }

        //Write method here to get individual exam with questions.
    }
}
