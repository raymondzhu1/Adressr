using Adressr.Library.Models;
using Adressr.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AdressrNET.Controllers
{
    [ApiController]
    [Route("user/preference")]
    [Authorize]
    public class PreferenceController : ControllerBase
    {
        private readonly IPreferenceService _preferenceService;

        public PreferenceController(IPreferenceService preferenceService)
        {
            _preferenceService = preferenceService;
        }

        [HttpGet]
        public async Task<ActionResult<PreferenceDTO>> GetPreference()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            PreferenceDTO? response = await _preferenceService.GetByUserIdAsync(userId);
            if(response is null)
            {
                return NotFound();
            }
            else
            {
                return response;
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePreference(PreferenceDTO preferenceRequest)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await _preferenceService.UpdatePreferenceAsync(userId, preferenceRequest);
            return NoContent();
        }
    }
}
