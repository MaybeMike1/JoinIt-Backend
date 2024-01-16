using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinIt_Backend.Features.Location.Models.Dtos
{
    public class AddressResponseDto
    {
        public string Message { get; set; } = string.Empty;

        public int StatusCode { get; set; }

        public List<AddressDto> Addresses { get; set; } = new();

        public AddressDto? Address { get; set; }

    }
}
