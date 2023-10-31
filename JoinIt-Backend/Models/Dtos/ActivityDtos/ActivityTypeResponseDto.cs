namespace JoinIt_Backend.Models.Dtos.ActivityDtos
{
    public class ActivityTypeResponseDto
    {
        public int StatusCode { get; set; }

        public List<ActivityType>? ActivityTypes { get; set; }

        public string Message { get; set; } = string.Empty;
    }
}
