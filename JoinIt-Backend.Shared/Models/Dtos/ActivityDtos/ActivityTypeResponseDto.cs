namespace JoinIt_Backend.Shared.Models.Dtos.ActivityDtos
{
    public class ActivityTypeResponseDto
    {
        public int StatusCode { get; set; }

        public List<ActivityType>? ActivityTypes { get; set; }

        public string Message { get; set; } = string.Empty;
    }
}
