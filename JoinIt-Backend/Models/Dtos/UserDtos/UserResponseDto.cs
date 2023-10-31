namespace JoinIt_Backend.Models.Dtos.UserDtos
{
    public class UserResponseDto
    {
        public int StatusCode { get; set; }

        public string Message { get; set; } = string.Empty;

        public User? User { get; set; }
    }
}
