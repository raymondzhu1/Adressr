using Microsoft.AspNetCore.Mvc;
using Adressr.Service.Interface;
using Adressr.Library.Models;

namespace AdressrNET.Controllers
{
    [Route("/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterUserRequest request)
        {
            UserDTO response = await _userService.RegisterAsync(request);
            return CreatedAtAction("register", new { id = response.UserID }, response); //This should go to the profile page of the new user. So do nameof(GetProfile) here instead of "register"
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            bool loginAttempt = await _userService.VerifyIdentityAsync(request.Identifier, request.Password);

            if (loginAttempt)
            {
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
