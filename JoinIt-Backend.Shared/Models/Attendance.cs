using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinIt_Backend.Shared.Models
{
    public class Attendance
    {
        [Key]
        public Guid Guid { get; set; } = Guid.NewGuid();

        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public User User { get; set; } = new();
        [ForeignKey("ActivityId")]
        public Guid ActivityId { get; set; }
        public Activity Activity { get; set; } = new();
    }
}
