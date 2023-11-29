namespace JoinIt_Backend.Shared.Models.Dtos.ActivityDtos
{
    public class ActivityResponseDto
    {
        /// <summary>
        /// Should be set if we are trying to get all activities.
        /// </summary>
        public List<Activity>? Activities { get; set; } = new();
        /// <summary>
        /// Should be set when we are creating new activity
        /// </summary>
        public Activity? NewActivity { get; set; }
        public int StatusCode { get; set; }

        public string Message { get; set; } = string.Empty;

    }
}
