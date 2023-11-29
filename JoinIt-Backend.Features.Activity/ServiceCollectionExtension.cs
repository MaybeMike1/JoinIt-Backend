using JoinIt_Backend.Features.Activity.Services;
using Microsoft.Extensions.DependencyInjection;

namespace JoinIt_Backend.Features.Activity
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddActivity(this IServiceCollection services)
        {
            services.AddScoped<IActivityService, ActivityService>();
            return services;
        }
    }
}