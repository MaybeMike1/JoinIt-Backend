using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinIt_Backend.Features.Location.Models.Dtos
{
    public class ZipDto
    {
        public Guid Id { get; set; }

        public string PostalCode { get; set; } = string.Empty;
    }
}
