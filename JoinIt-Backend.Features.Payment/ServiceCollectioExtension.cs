using JoinIt_Backend.Features.Payment.Helpers;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace JoinIt_Backend.Features.Payment
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddPaymentService(this IServiceCollection services)
        {
            services.AddScoped<IPaymentHelper , PaymentHelper>();
            services.AddMassTransit(t =>
            {
                t.UsingRabbitMq((context, cfg) =>
                {

                    cfg.Host(new Uri("amqps://xqxdudxe:NSHT-pUOE6P8CSD2bRP-Zukqhmu8AtZR@cow.rmq2.cloudamqp.com/xqxdudxe"), "/", h =>
                    {
                        h.Username("xqxdudxe");
                        h.Password("NSHT-pUOE6P8CSD2bRP-Zukqhmu8AtZR");
                    });
                    cfg.ConfigureEndpoints(context);
                });
            });
            return services;
        }
    }
}