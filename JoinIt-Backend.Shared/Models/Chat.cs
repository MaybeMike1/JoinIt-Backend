//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace JoinIt_Backend.Shared.Models
//{
//    public class Chat
//    {
//        [Key]
//        public Guid ChatId { get; set; }
//        [ForeignKey("MessageId")]
//        public Guid MessageId { get; set; }

//        public Chat ChatRoom { get; set; } = new Chat();

//        public List<Message> Messages { get; set; } = new();
//    }
//}
