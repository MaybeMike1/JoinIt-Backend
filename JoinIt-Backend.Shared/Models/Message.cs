//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace JoinIt_Backend.Shared.Models
//{
//    public class Message
//    {
//        [Key]
//        public Guid MessageId { get; set; }
//        [ForeignKey("UserId")]
//        public Guid UserId { get; set; }
//        public User User { get; set; } = new User();
//        [ForeignKey("ChatId")]
//        public Guid ChatId { get; set; }
//        public Chat Chat { get; set; } = new();
//        public string Value { get; set; } = string.Empty;
//    }
//}
