using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JoinIt_Backend.Models
{
    [Table("Addresses")]
    public class Address
    {
        [Key]
        public Guid Guid { get; set; }

        public string StreetName { get; set; } = string.Empty;

        public int StreetNumber { get; set; }

        public Zip Zip { get; set; } = new();

        public int? Floor { get; set; }
    }
}
