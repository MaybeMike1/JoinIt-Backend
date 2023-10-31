namespace JoinIt_Backend.Models.Dtos
{
    public class AuthenticationRequestDto
    {
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
