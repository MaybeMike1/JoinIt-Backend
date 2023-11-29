using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JoinIt_Backend.Models
{
    [Table("Activities")]
    public class Activity
    {
        [Key]
        public Guid Guid { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Address Address { get; set; } = new();

        public string? VenueName { get; set; }

        public DateTime Date { get; set; }
        public ICollection<Attendance> Attendants { get; set; } = new List<Attendance>();

        public ActivityType ActivityType { get; set; } = new();

        public bool IsCancelled { get; set; } = false;

    }

}
