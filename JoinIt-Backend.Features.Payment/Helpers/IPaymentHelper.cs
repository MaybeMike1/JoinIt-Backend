
using JoinIt_Backend.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinIt_Backend.Features.Payment.Helpers
{
    public interface IPaymentHelper
    {
        decimal CalculateAttendanceCost(Activity activity);
    }
    public class PaymentHelper : IPaymentHelper
    {
        public decimal CalculateAttendanceCost(Activity activity)
        {
            var attendents = activity.Attendants.Count;
            var attendanceCost = attendents / activity.ActivityCosts;
            return attendanceCost;
        }
    }
}
