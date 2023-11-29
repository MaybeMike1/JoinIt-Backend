
namespace JoinIt_Backend.Features.Activity.Models.Dtos
{
    public class ActivityResponseDto
    {
        /// <summary>
        /// Should be set if we are trying to get all activities.
        /// </summary>
        public List<ActivityDto>? Activities { get; set; } = new();
        /// <summary>
        /// Should be set when we are creating new activity
        /// </summary>
        public ActivityDto? NewActivity { get; set; }
        public int StatusCode { get; set; }

        public string Message { get; set; } = string.Empty;

    }
}
