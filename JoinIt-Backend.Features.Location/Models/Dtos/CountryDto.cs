using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinIt_Backend.Features.Location.Models.Dtos
{
    public class CountryDto
    {
        public Guid Id { get; set; }

        public string Country { get; set; } = string.Empty;

        public List<string> PostalCodes { get; set; } = new();
    }
}
