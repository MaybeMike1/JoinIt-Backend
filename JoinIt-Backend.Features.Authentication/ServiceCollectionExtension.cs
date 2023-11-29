using JoinIt_Backend.Features.Authentication.Services;
using Microsoft.Extensions.DependencyInjection;

namespace JoinIt_Backend.Features.Authentication
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddAuth(this IServiceCollection services)
        {
            services.AddScoped<ICryptService, CryptService>();
            services.AddScoped<IAuthProvider, AuthProvider>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }

    }
}