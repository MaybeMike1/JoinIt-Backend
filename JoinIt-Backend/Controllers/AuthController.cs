using JoinIt_Backend.Features.Authentication.Models.Dtos;
using JoinIt_Backend.Models;
using JoinIt_Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JoinIt_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthProvider _authProvider;
        private readonly ICryptService _cryptService;
        public AuthController(ICryptService cryptService, IAuthProvider authProvider)
        {
            _cryptService = cryptService;
            _authProvider = authProvider;
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody] AuthenticationRequestDto credentials)
        {
            var response = await _authProvider.Login(credentials);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto userDto)
        {
            try
            {
                var response = await _authProvider.Register(userDto);
                return StatusCode(response.StatusCode, response);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Ok(ex.Message);
            }

        }
    }
}
