using Microsoft.EntityFrameworkCore;

namespace JoinIt_Backend.Models
{
    public class Activity
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Address Address { get; set; } = new();

        public string? VenueName { get; set; }

        public DateTime Date { get; set; }

        public List<User> Attendants { get; set; } = new();

        public ActivityType ActivityType { get; set; } = new();

        public bool IsCancelled { get; set; } = false;

    }

}
