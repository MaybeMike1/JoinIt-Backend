using JoinIt_Backend.Features.Location.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinIt_Backend.Features.Location.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _addressService;

        public CountryController(ICountryService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet, ActionName("Get")]
        public async Task<IActionResult> Get()
        {
            var response = await _addressService.GetAllCountries();
            return StatusCode(response.StatusCode, response);
        }

    }
}
