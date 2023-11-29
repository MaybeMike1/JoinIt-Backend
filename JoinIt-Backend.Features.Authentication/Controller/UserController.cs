using JoinIt_Backend.Features.Authentication.Services;
using JoinIt_Backend.Shared.Models.Dtos.UserDtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinIt_Backend.Features.Authentication.Controller
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser([FromQuery] string userGuid)
        {
            if (!Guid.TryParse(userGuid, out Guid res))
                return BadRequest($"{nameof(userGuid)} is not valid.");

            var response = await _userService.GetUser(res);
            return StatusCode(response.StatusCode, response);

        }

        [HttpPatch]
        public async Task<IActionResult> UpdateUser([FromQuery] string userGuid, [FromBody] UpdateUserDto updateUserDto)
        {
            if (!Guid.TryParse(userGuid, out Guid res))
                return BadRequest($"{nameof(userGuid)} is not valid.");

            var response = await _userService.UpdateUser(res, updateUserDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser([FromQuery] string userGuid)
        {
            if (!Guid.TryParse(userGuid, out Guid res))
                return BadRequest($"{nameof(userGuid)} is not valid.");
            var response = await _userService.DeleteUser(res);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("change-password")]
        public async Task<IActionResult> ChangePassword([FromQuery] string userGuid, [FromBody] UpdateUserPasswordDto updateUserPasswordDto)
        {
            if (!Guid.TryParse(userGuid, out Guid res))
                return BadRequest($"{nameof(userGuid)} is not valid.");
            var response = await _userService.ChangePassword(res, updateUserPasswordDto);
            return StatusCode(response.StatusCode, response);
        }
    }
}
