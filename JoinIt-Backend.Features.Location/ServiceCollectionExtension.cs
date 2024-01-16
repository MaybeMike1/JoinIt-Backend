using JoinIt_Backend.Features.Location.Services;
using Microsoft.Extensions.DependencyInjection;

namespace JoinIt_Backend.Features.Location
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddLocation(this IServiceCollection services)
        {
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IZipService, ZipService>();
            services.AddScoped<IAddressService, AddressService>();
            return services;
        }
    }
}