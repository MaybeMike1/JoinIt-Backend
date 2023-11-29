namespace JoinIt_Backend.Shared.Models.Dtos.UserDtos
{
    public class UpdateUserPasswordDto
    {
        public string CurrentPassword { get; set; } = string.Empty;

        public string NewPassword { get; set; } = string.Empty;
    }
}
