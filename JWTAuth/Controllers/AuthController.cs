using Microsoft.AspNetCore.Mvc;
using JWTAuth.Bussiness.Authentication;
using JWTAuth.Bussiness.Interfaces;
using JWTAuth.Bussiness.DTOs;
using Microsoft.AspNetCore.Authorization;
namespace JWTAuth.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        public AuthController(IAuthManager authManager)
        {
            _authManager = authManager;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var response = await _authManager.LoginAsync(request.Email, request.Password);
                return Ok(response);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] string token)
        {
            try
            {
                var response = await _authManager.RefreshTokenAsync(token);
                return Ok(response);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        [Authorize]
        [HttpGet("getdata")]
        public async Task<IActionResult> GetData()
        {
            try
            {
                Dictionary<String,String> data = new Dictionary<string, string>();
                data["Users"] = "Log";
                return Ok(data);
            }
            catch
            {
                return Unauthorized();
            }
        }

    }
    
    
}
