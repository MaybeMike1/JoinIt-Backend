using JoinIt_Backend.Features.Activity.Models.Dtos;
using JoinIt_Backend.Features.Activity.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinIt_Backend.Features.Activity.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ActivityTypeController : ControllerBase
    {
        private readonly IActivityService _activityService;

        public ActivityTypeController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetActivityType(Guid guid)
        {
            var res = await _activityService.GetActivityTypeByGuid(guid);
            return StatusCode(res.StatusCode, res);

        }

        [HttpGet]
        public async Task<IActionResult> GetActivityTypes()
        {
            var res = await _activityService.GetActivityTypes();
            return StatusCode(res.StatusCode, res);

        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity([FromBody] ActivityRequestDto dto)
        {
            var res = await _activityService.CreateActivityType(dto);
            return StatusCode(res.StatusCode, res);
        }
    }
}
