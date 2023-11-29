using Microsoft.Extensions.DependencyInjection;

namespace JoinIt_Backend.Features.Location
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddLocation(this IServiceCollection services)
        {
            return services;
        }
    }
}