using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinIt_Backend.Features.Location.Models.Dtos
{
    public class AddressRequestDto
    {
        public string StreetName { get; set; } = string.Empty;

        public int StreetNumber { get; set; }

        public int? Floor { get; set; } = null;

        public Guid ZipGuid { get; set; } = new();
    }
}
