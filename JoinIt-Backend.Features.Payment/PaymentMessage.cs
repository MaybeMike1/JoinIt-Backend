using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinIt_Backend.Features.Payment
{
    public class PaymentMessage
    {
        public Guid PaymentId { get; set; }

        public Guid UserGuid { get; set; }

        public Guid ActivityGuid { get; set; }

        public bool IsPaid { get; set; }
        
        public DateTime? PaymentDate { get; set; }

    }
}
