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
    public class WorkTaskController : ControllerBase
    {
        private readonly IWorkTaskService _workTaskService;

        public WorkTaskController(IWorkTaskService workTaskService)
        {
            _workTaskService = workTaskService;
        }

        [HttpGet("dashboard")]
        public async Task<List<DashboardExpTaskResponse>> DashboardGetTasks()
        {
            List<DashboardExpTaskResponse> response = await _workTaskService.GetOpenTasksForUserAsync(User.GetUserId());
            return response;
        }
    }
}
