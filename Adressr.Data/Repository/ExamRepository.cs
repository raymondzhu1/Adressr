using Adressr.Data.Model;
using Adressr.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Adressr.Data.Repository
{
    public class ExamRepository : IExamRepository
    {
        private readonly AdressrContext _context;

        public ExamRepository(AdressrContext context)
        {
            _context = context;
        }

        public Task<List<Exam>> GetExamsInvitedToByUserIdAsync(int userId)
        {
            return _context.Exams.AsNoTracking().Where(exam => exam.Private && exam.PrivateExamParticipants.Any(user => user.UserID == userId)).OrderBy(exam => exam.Title).ToListAsync();
        }
    }
}
