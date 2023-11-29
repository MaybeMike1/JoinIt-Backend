using JoinIt_Backend.Shared.Models;

namespace JoinIt_Backend.Features.Activity.Models.Dtos
{
    public class ActivityTypeResponseDto
    {
        public int StatusCode { get; set; }

        public List<ActivityType>? ActivityTypes { get; set; }

        public string Message { get; set; } = string.Empty;
    }
}
