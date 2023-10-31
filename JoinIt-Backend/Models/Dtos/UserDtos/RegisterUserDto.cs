namespace JoinIt_Backend.Models.Dtos.UserDtos
{
    public class RegisterUserDto
    {
        public string Email { get; set; } = string.Empty;

        public string PlainPassword { get; set; } = string.Empty;

        public string Phonenumber { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string? LastName { get; set; } = string.Empty;

    }
}
