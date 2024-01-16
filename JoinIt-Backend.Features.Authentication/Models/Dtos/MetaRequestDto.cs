using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinIt_Backend.Features.Authentication.Models.Dtos
{
    public class MetaRequestDto
    {
        public string MetaUserId { get; set; } = string.Empty;

        public string MetaUserFullName { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;
    }
}
