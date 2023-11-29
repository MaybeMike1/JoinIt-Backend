using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JoinIt_Backend.Shared.Models
{
    [Table("Countries")]
    public class Country
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = string.Empty;

        public List<Zip> ZipCodes { get; set; } = new();
    }
}
