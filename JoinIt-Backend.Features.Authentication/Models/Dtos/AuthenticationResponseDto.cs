namespace JoinIt_Backend.Features.Authentication.Models.Dtos
{
    public class AuthenticationResponseDto
    {
        public Guid? Guid { get; set; }

        public string? Token { get; set; }

        public string? Email { get; set; }

        public string Message { get; set; } = string.Empty;

        public int StatusCode { get; set; }
    }
}
