using Microsoft.AspNetCore.Mvc;
using Adressr.Service.Interface;
using Adressr.Library.Models;
using Adressr.Library;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace AdressrNET.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JWTSettings _jwtSettings;

        public UserController(IUserService userService, IOptions<JWTSettings> jwtSettings)
        {
            _userService = userService;
            _jwtSettings = jwtSettings.Value;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterUserRequest request)
        {
            //Might need to add token here as well if we want user immediately signed in on registration. Otherwise keep as is.
            UserDTO response = await _userService.RegisterAsync(request);
            return CreatedAtAction("register", new { id = response.UserID }, response); //This should go to the profile page of the new user. So do nameof(GetProfile) here instead of "register"
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            UserDTO? loginAttempt = await _userService.VerifyIdentityAsync(request.Identifier, request.Password);

            if (loginAttempt is null)
            {
                return Unauthorized();
            }
            else
            {
                Claim[] claims = [new Claim(ClaimTypes.NameIdentifier, loginAttempt.UserID.ToString())];
                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: _jwtSettings.Issuer,
                    audience: _jwtSettings.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
        }
    }
}
