using BookMyShow.DTO;
using BookMyShow.Models;
using BookMyShow.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookMyShow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IConfiguration _config;
        public readonly IAuthRepository _authService;
        public AuthController(IConfiguration config, IAuthRepository authService)
        {
            _config = config;
            _authService = authService;
        }
        [HttpPost("Logout")]
        public async Task<IActionResult>Logout()
        {
            var result = _authService.Logout(HttpContext);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("Signup")]
        public async Task<IActionResult> Signup([FromBody] SignupDto userData)
        {
            if (userData == null)
            {
                return BadRequest(new { message = "User Data is required" });
            }
            if (await _authService.EmailExistsAsync(userData.Email))
            {
                return BadRequest(new { message = "Email Already Exists" });
            }
            try
            {
                var newUser = await _authService.SignupAsync(userData);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during signup." });
            }
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto userData)
        {
            var user = await _authService.LoginAsync(userData,HttpContext);
            if (user != null)
            {
                return Ok(new
                {
                    message = "Login successful",
                    user = user
                });
            }
            else
            {
                return BadRequest("User not found");
            }
        }
    }
}