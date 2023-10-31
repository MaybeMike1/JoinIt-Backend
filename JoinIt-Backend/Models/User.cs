using System.ComponentModel.DataAnnotations.Schema;

namespace JoinIt_Backend.Models
{
    public class User
    {
        public int Id { get; set; }

        public Guid Guid { get; set; } = Guid.NewGuid();

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Username { get; set;} = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public int PostalCode { get; set; }

        public List<Activity> ActiveActivities { get; set; } = new();

        [NotMapped]
        public List<Activity> ArchivedActivities { get; set; } = new();


        public DateTime SignUpDate { get; set; } = DateTime.Now;

        public DateTime LastLoginDate { get; set; } = DateTime.Now;
    }
}
