using AtmoSync.API.Interfaces.IServices;
using AtmoSync.Shared.Models.DtoModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AtmoSync.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var response = await _authService.RegisterAsync(dto);

            return StatusCode(response.Code,response);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var response = await _authService.LoginAsync(dto);

            return StatusCode( response.Code,response);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto dto)
        {
            var response = await _authService.RefreshTokenAsync(dto);

            return StatusCode(response.Code,response);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized(
                    new
                    {
                        Message = "Invalid token."
                    }
                );
            }
            var response = await _authService.LogoutAsync(long.Parse(userId));
            return StatusCode(response.Code,response);
        }
    }
}