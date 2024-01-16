using MassTransit;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinIt_Backend.Features.Payment.Services
{
    public class PaymentPublishService : BackgroundService
    {
        private readonly IBus _bus;

        public PaymentPublishService(IBus bus)
        {
            _bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var endpoint = await _bus.GetSendEndpoint(new Uri(""));

            await endpoint.Send(new
            {
                Message = "hahahah"
            }, stoppingToken);
        }
    }
}
