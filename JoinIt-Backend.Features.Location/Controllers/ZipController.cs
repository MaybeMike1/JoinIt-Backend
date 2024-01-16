using JoinIt_Backend.Features.Location.Services;
using JoinIt_Backend.Shared.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinIt_Backend.Features.Location.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ZipController : ControllerBase
    {
        private readonly IZipService _zipService; 

        public ZipController(IZipService zipService)
        {
            _zipService = zipService;
        }

        [HttpGet, ActionName("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _zipService.GetZips();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet, ActionName("GetCountryZips")]
        public async Task<IActionResult> GetByCountryName(string countryName)
        {
            var response = await _zipService.GetZipsByCountry(countryName);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet, ActionName("Get")]
        public async Task<IActionResult> Get(Guid id)
        {
            var response = await _zipService.GetZipById(id);
            return StatusCode(response.StatusCode, response);
        }

    }
}
