using JoinIt_Backend.Shared.Models;

namespace JoinIt_Backend.Models.Dtos.ActivityDtos
{
    public class CreateActivityDto
    {
        public Address Address { get; set; } = new();

        public string ActivityName { get; set; } = string.Empty;

        public int ActivityTypeId { get; set; }

        public ActivityType ActivityType { get; set; } = new();

        public string Description { get; set; } = string.Empty;

        public string VenueName { get; set; } = string.Empty;

        public Guid ActivityCreatorGuid { get; set; } = new();

        public string DateString { get; set; } = string.Empty;

        
    }
}
