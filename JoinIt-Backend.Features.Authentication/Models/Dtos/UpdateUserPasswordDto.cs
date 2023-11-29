namespace JoinIt_Backend.Features.Authentication.Models.Dtos
{
    public class UpdateUserPasswordDto
    {
        public string CurrentPassword { get; set; } = string.Empty;

        public string NewPassword { get; set; } = string.Empty;
    }
}
