using JoinIt_Backend.Features.Activity.Models.Dtos;
using JoinIt_Backend.Features.Activity.Services;
using Microsoft.AspNetCore.Mvc;

namespace JoinIt_Backend.Features.Activity.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityService _activityService;

        public ActivityController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        [HttpGet, ActionName("Get")]
        public async Task<ActionResult> Get()
        {
            var response = await _activityService.GetActivities();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet, ActionName("GetUserActivities")]
        public async Task<IActionResult> GetUserActivities(Guid userGuid)
        {
            var response = await _activityService.GetUserActivities(userGuid);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost, ActionName("Create")]
        public async Task<ActionResult> Create([FromBody]CreateActivityDto createActivityDto, Guid creatorGuid)
        {
            var response = await _activityService.CreateActivity(createActivityDto, creatorGuid);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut, ActionName("Enroll")]
        public async Task<IActionResult> EnrollActivity(Guid userGuid, Guid activityGuid)
        {
            var response = await _activityService.EnrollActivity(userGuid, activityGuid);
            return StatusCode(response.StatusCode, response);
        }
    }
}
