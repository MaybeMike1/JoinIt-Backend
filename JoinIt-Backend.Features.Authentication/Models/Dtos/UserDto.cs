using JoinIt_Backend.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinIt_Backend.Features.Authentication.Models.Dtos
{
    public class UserDto
    {
        public Guid Guid { get; set; } = new();

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public int PostalCode { get; set; }


        public static UserDto MapToUserDto(User user)
        {
            return new UserDto
            {
                Guid = user.Guid,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                PostalCode = user.PostalCode,
            };
        }
    }
}
