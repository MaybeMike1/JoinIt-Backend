using JoinIt_Backend.Features.Authentication.Models.Dtos;
using JoinIt_Backend.Features.Authentication.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinIt_Backend.Features.Authentication.Controller
{
    [Route("api/auth/[action]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthProvider _authProvider;

        public AuthenticationController(IAuthProvider authProvider)
        {
            _authProvider = authProvider;
        }
        [HttpPost, ActionName("Authenticate")]

        public async Task<IActionResult> Authenticate([FromBody]AuthenticationRequestDto authenticationRequestDto)
        {
            var response = await _authProvider.Login(authenticationRequestDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost, ActionName("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto userDto) {
            var response = await _authProvider.Register(userDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost, ActionName("facebook-register")]
        public async Task<IActionResult> AuthenticateFacebook([FromBody]MetaRequestDto metaRequestDto)
        {
            var response = await _authProvider.FacebookLogin(metaRequestDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet, ActionName("google-register")]
        public async Task<IActionResult> AuthenticateGoogle()
        {
            return Ok("Google");
        }

        [HttpGet, ActionName("forgot-password")]
        public  ActionResult ForgotPassword()
        {

            return StatusCode(200);
        }
    }
}
