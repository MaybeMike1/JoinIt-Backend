using JoinIt_Backend.Shared.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinIt_Backend.Features.Activity.Models.Dtos
{
    public class ActivityDto
    {
        public Guid ActivityGuid { get; set; }

        public string ActivityName { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string ActivityType { get; set; } = string.Empty;

        public string ActivityTypeDescription { get; set; } = string.Empty;

        public string FriendlyStartDate { get; set; } = string.Empty;

        /// <summary>
        /// List of Guids storing the users attendending to current Activity.
        /// </summary>
        public List<Guid> Attendants { get; set; } = new();


        public static ActivityDto MapToActivityDto(Shared.Models.Activity source)
        {
            return new ActivityDto
            {
                ActivityGuid = source.Guid,
                ActivityName = source.Name,
                ActivityType = source.ActivityType.Type,
                ActivityTypeDescription = source.Description,
                Attendants = source.Attendants.Select(x => x.UserId).ToList(),
                FriendlyStartDate = source.Date.ToString("dddd, MMMM dd, yyyy HH:mm", new CultureInfo("da-DK")),
                Location = string.Format("{0} {1}", source.Address.StreetName, source.Address.StreetNumber.ToString()),
            };
        }
    }
}
