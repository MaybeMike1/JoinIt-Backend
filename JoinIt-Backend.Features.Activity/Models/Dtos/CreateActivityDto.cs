using JoinIt_Backend.Shared.Models;

namespace JoinIt_Backend.Features.Activity.Models.Dtos
{
    public class CreateActivityDto
    {

        public string ActivityName { get; set; } = string.Empty;

        public Guid ActivityTypeGuid { get; set; }

        public decimal ActivityCost { get; set; }

        public string Description { get; set; } = string.Empty;

        public Guid AddressGuid { get; set; }

        public string VenueName { get; set; } = string.Empty;

        public Guid ActivityCreatorGuid { get; set; } = new();

        public string DateString { get; set; } = string.Empty;

        public int MaxParticipants { get; set; }

        public static Shared.Models.Activity MapToActivity(CreateActivityDto source)
        {
            return new Shared.Models.Activity
            {
                Description = source.Description,
                IsCancelled = false,
                VenueName = source.VenueName,
                Name = source.ActivityName,
                Date = DateTime.Parse(source.DateString),
                ActivityCosts = source.ActivityCost,
                ActivityCapacity = source.MaxParticipants
                
            };
        }
    }
}
