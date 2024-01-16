using JoinIt_Backend.Features.Location.Models.Dtos;
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
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _addressService.GetAddresses();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(Guid guid)
        {
            var response = await _addressService.GetAddressById(guid);
            return StatusCode(response.StatusCode, response);
        }


        [HttpPost]
        public async Task<IActionResult> Create(AddressRequestDto addressRequestDto)
        {
            var response = await _addressService.CreateNewAddress(addressRequestDto);
            return StatusCode(response.StatusCode, response);
        }
    }
}
