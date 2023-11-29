using JoinIt_Backend.Shared.Models;

namespace JoinIt_Backend.Features.Authentication.Models.Dtos
{
    public class UserResponseDto
    {
        public int StatusCode { get; set; }

        public string Message { get; set; } = string.Empty;

        public User? User { get; set; }
    }
}
