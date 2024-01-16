using JoinIt_Backend.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinIt_Backend.Features.Location.Models.Dtos
{
    public class CountryResponseDto
    {
        public int StatusCode { get; set; }

        public string Message { get; set; } = string.Empty;

        public Country? Country { get; set; } = new();

        public List<CountryDto> Countries { get; set; } = new();
    }
}
