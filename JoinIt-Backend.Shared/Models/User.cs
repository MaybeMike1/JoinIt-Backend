using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JoinIt_Backend.Shared.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public Guid Guid { get; set; } = Guid.NewGuid();

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public int PostalCode { get; set; }

        /// <summary>
        /// Identifier provided from OAuth when using Facebook or Google Authentication.
        /// </summary>
        public string ExternalIdentityId { get; set; } = string.Empty;

        public List<Attendance> Attendances { get; set; } = new();

        public List<Activity> ActiveActivities { get; set; } = new();

        [NotMapped]
        public List<Activity> ArchivedActivities { get; set; } = new();


        public DateTime SignUpDate { get; set; } = DateTime.Now;

        public DateTime LastLoginDate { get; set; } = DateTime.Now;
    }
}
