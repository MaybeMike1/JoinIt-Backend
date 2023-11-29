using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JoinIt_Backend.Shared.Models
{
    [Table("Zips")]
    public class Zip
    {
        [Key]
        public Guid Guid { get; set; } = Guid.NewGuid();

        public string PostalCode { get; set; } = string.Empty;

        public Country Country { get; set; } = new();

        public List<Address> Addresses { get; set; } = new();

    }
}
