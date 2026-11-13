using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Adressr.Service.Interface;
using Adressr.Library.Models;
using AdressrNET.Extensions;

namespace AdressrNET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExperienceController : ControllerBase
    {
        private readonly IExperienceService _experienceService;

        public ExperienceController(IExperienceService experienceService)
        {
            _experienceService = experienceService;
        }

        [HttpGet("experiencemanager")]
        public async Task<ActionResult<List<ExperienceResponse>>> GetExperiences()
        {
            List<ExperienceResponse> response = await _experienceService.GetExperiencesForManager(User.GetUserId());
            return response;
        }

        [HttpPost("experiencemanager")]
        public async Task<ActionResult<ExperienceResponse>> AddExperience(CreateExperienceRequest request)
        {
            ExperienceResponse response = await _experienceService.AddExperienceAsync(User.GetUserId(), request);
            return CreatedAtAction("experiencemanager", new { id = response.ExperienceID }, response);
        }
    }
}
