using JoinIt_Backend.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinIt_Backend.Features.Location.Models.Dtos
{
    public class ZipResponseDto
    {
        public int StatusCode { get; set; }

        public Zip? Zip { get; set; }

        public List<ZipDto> Zips { get; set; } = new(); 

        public string Message { get; set; } = string.Empty;
    }
}
