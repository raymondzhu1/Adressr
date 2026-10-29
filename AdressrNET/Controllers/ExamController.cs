using Adressr.Library.Models;
using Adressr.Service.Interface;
using AdressrNET.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdressrNET.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ExamController : ControllerBase
    {
        private readonly IExamService _examService;

        public ExamController(IExamService examService)
        {
            _examService = examService;
        }

        [HttpGet("dashboard")]
        public async Task<ActionResult<List<DashboardExamResponse>>> GetDashboardExams()
        {
            List<DashboardExamResponse> response = await _examService.GetExamsInvitedToAsync(User.GetUserId());
            return response;
        }
    }
}
