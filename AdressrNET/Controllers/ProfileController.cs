using Adressr.Library.Models;
using Adressr.Service.Interface;
using AdressrNET.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdressrNET.Controllers
{
    [Route("user/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet("{username}")]
        [AllowAnonymous]
        public async Task<ActionResult<ProfileResponse>> GetProfile(string username)
        {
            int? viewerUserId = User.Identity?.IsAuthenticated == true ? User.GetUserId() : null;

            ProfileResponse? profile = await _profileService.GetByUsernameAsync(username, viewerUserId);
            if(profile is null)
            {
                return NotFound();
            }
            else
            {
                return profile;
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfile(ProfileRequest request)
        {
            await _profileService.UpdateProfileAsync(User.GetUserId(), request);
            return NoContent();
        }
    }
}
