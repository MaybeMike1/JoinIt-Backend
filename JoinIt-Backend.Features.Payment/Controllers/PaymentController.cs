using JoinIt_Backend.Features.Payment.Helpers;
using JoinIt_Backend.Shared.Data;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinIt_Backend.Features.Payment.Controllers
{
    [Route("api/[controller]/[action]")]
    public class PaymentController : ControllerBase
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IPaymentHelper _paymentHelper;
        private readonly DatabaseContext _databaseContext;

        public PaymentController(ISendEndpointProvider sendEndpointProvider, IPaymentHelper paymentHelper, DatabaseContext databaseContext)
        {
            _sendEndpointProvider = sendEndpointProvider;
            _paymentHelper = paymentHelper;
            _databaseContext = databaseContext;
        }

        [HttpPost, ActionName("sendPayment")]
        public async Task<IActionResult> SendPayment()
        {
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:test"));
            await endpoint.Send<PaymentMessage>(new PaymentMessage { UserGuid = Guid.NewGuid(), ActivityGuid = Guid.NewGuid(), PaymentId = Guid.NewGuid(), IsPaid = false});
            return Accepted();
        }

    }
}
