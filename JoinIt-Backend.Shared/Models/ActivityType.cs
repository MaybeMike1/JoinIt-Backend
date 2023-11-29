using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JoinIt_Backend.Shared.Models
{
    [Table("ActivityTypes")]
    public class ActivityType
    {
        [Key]
        public Guid Guid { get; set; }

        public string Type { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}
