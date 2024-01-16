using Microsoft.Extensions.DependencyInjection;

namespace JoinIt_Backend.Features.Chat
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddChat(this IServiceCollection services)
        {
            services.AddSignalR();
            return services;
        }
    }
}