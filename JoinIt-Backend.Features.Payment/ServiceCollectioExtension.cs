using Microsoft.Extensions.DependencyInjection;

namespace JoinIt_Backend.Features.Payment
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddPayment(this IServiceCollection services)
        {
            return services;
        }
    }
}