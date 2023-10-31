using JoinIt_Backend.Models;
using JoinIt_Backend.Models.Dtos.ActivityDtos;
using JoinIt_Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JoinIt_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        public IActivityContextProvider _activityContextProvider { get; set; }

        public ActivityController(IActivityContextProvider activityContextProvider)
        {
            _activityContextProvider = activityContextProvider;
        }

        [HttpGet("types")]
        public async Task<IActionResult> GetActivityTypes()
        {
            var response = await _activityContextProvider.GetActivityTypes();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _activityContextProvider.GetActivities();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("nearby")]
        public async Task<IActionResult> GetNearby([FromRoute] string postalCode)
        {
            var response = await _activityContextProvider.GetNearbyActivities(postalCode);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity([FromQuery] string userGuid, [FromBody] CreateActivityDto createActivityDto)
        {
            if (!Guid.TryParse(userGuid, out Guid res))
                return BadRequest($"{nameof(userGuid)} must be valid");

            var response = await _activityContextProvider.CreateActivity(createActivityDto, res);
            return StatusCode(200 ,response);
        }
    }
}
