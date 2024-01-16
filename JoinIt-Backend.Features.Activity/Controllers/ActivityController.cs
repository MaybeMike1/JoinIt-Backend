using JoinIt_Backend.Features.Activity.Models.Dtos;
using JoinIt_Backend.Features.Activity.Services;
using JoinIt_Backend.Shared.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JoinIt_Backend.Features.Activity.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityService _activityService;
        private readonly DatabaseContext _databaseContext;

        public ActivityController(IActivityService activityService, DatabaseContext databaseContext)
        {
            _activityService = activityService;
            _databaseContext = databaseContext;
        }


        [HttpGet, ActionName("Get-Test")]
        public async Task<IActionResult> GetTest()
        {
            var some = _databaseContext.Activities.FromSqlRaw("SELECT * FROM Activities").ToList();
            return Ok(some);
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

        [HttpPut, ActionName("Detach")]
        public async Task<IActionResult> DetachActivity(Guid userGuid, Guid activityGuid)
        {
            var response = await _activityService.DetachActivity(userGuid, activityGuid);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet, ActionName("CreatedActivities")]
        public async Task<IActionResult> GetCreatedActivities(Guid userGuid)
        {
            var response = await _activityService.GetCreatedActivities(userGuid);
            return StatusCode(response.StatusCode, response);
        }
    }
}
